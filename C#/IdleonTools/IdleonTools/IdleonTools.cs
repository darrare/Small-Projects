using IdleonTools.Tools;
using System.Runtime.InteropServices;
using Timer = System.Timers.Timer;
using Gma.System.MouseKeyHook;
using System.Drawing.Imaging;


namespace IdleonTools
{
    public partial class IdleonTools : Form
    {
        FishingMinigameTool fishingMinigameTool;
        ChoppinMinigameTool choppinMinigameTool;

        public IdleonTools()
        {
            InitializeComponent();
            Logger.LogTextBox = logTextBox;
            Logger.StateTextBox = stateHistoryTextBox;
        }

        private void isFishingEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (isFishingEnabled.Checked)
            {
                if (fishingMinigameTool != null)
                {
                    fishingMinigameTool.Stop();
                }
                fishingMinigameTool = FishingMinigameTool.Start();
            }
            else
            {
                if (fishingMinigameTool != null)
                {
                    fishingMinigameTool.Stop();
                    fishingMinigameTool = null;
                }
            }
        }

        private void choppingToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (choppingToggle.Checked)
            {
                if (choppinMinigameTool != null)
                {
                    choppinMinigameTool.Stop();
                }
                choppinMinigameTool = ChoppinMinigameTool.Start();
            }
            else
            {
                if (choppinMinigameTool != null)
                {
                    choppinMinigameTool.Stop();
                    choppinMinigameTool = null;
                }
            }
        }

        #region stuff for the choppin minigame

        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point lpPoint);

        bool captureInProgress = false;

        CancellationTokenSource cancellationTokenSource;

        private async void startCaptureButton_Click(object sender, EventArgs e)
        {
            if (!captureInProgress)
            {
                captureInProgress = true;
                Logger.StateToForm("RIGHT CLICK in the green bar of the mini game to record it's position...");

                using (cancellationTokenSource = new CancellationTokenSource())
                {
                    using (IKeyboardMouseEvents m_events = Hook.GlobalEvents())
                    {
                        m_events.MouseClick += HookManager_MouseClick;

                        await Task.Factory.StartNew(() =>
                        {
                            while (!cancellationTokenSource.Token.IsCancellationRequested)
                            {
                                Thread.Sleep(100);
                            }
                        }, cancellationTokenSource.Token);
                    }
                }
                captureInProgress = false;
            }
        }

        private void HookManager_MouseClick(object sender, MouseEventArgs e)
        {
            // Only do things on a right click
            if (e.Button == MouseButtons.Left)
                return;

            cancellationTokenSource.Cancel();

            //Get the cursor position
            Point cursor = new Point();
            GetCursorPos(ref cursor);

            // Capture the image from the mouse position
            Bitmap captured = new Bitmap(400, 30, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(captured))
            {
                g.CopyFromScreen(cursor.X - captured.Width / 2, cursor.Y - captured.Height / 2, 0, 0, captured.Size);
            }

            // Calculate the actual left, right, and bottom of the UI
            // Pick a point, and go down with a bar including that X and x - 3 and x + 3 until one of them is black.
            Point currentPoint = new Point(captured.Width / 2, captured.Height / 2);
            Color[] colors = new Color[3]; //left, middle, right
            do
            {
                currentPoint.Y -= 1;
                if (currentPoint.Y < 0)
                {
                    Logger.LogToForm("Unable to find black bar at bottom of image... Try again...");
                    return;
                }
                colors[0] = captured.GetPixel(currentPoint.X - 3, currentPoint.Y);
                colors[1] = captured.GetPixel(currentPoint.X, currentPoint.Y);
                colors[2] = captured.GetPixel(currentPoint.X + 3, currentPoint.Y);
            } while (colors.All(t => t.ToArgb() != Color.Black.ToArgb()));

            Point rightPoint = new Point(currentPoint.X, currentPoint.Y);
            Point leftPoint = new Point(currentPoint.X, currentPoint.Y);
            Point lastBlackRightPoint = new Point(currentPoint.X, currentPoint.Y);
            Point lastBlackLeftPoint = new Point(currentPoint.X, currentPoint.Y);

            // Found bottom, now migrate right
            int counter = 0;
            int counterLimit = 6;
            do
            {
                rightPoint.X += 1;
                counter++;
                if (rightPoint.X >= captured.Width)
                {
                    Logger.LogToForm("Unable to find black bar at right of image... Try again...");
                    return;
                }
                if (captured.GetPixel(rightPoint.X, rightPoint.Y).ToArgb() == Color.Black.ToArgb())
                {
                    counter = 0;
                    lastBlackRightPoint.X = rightPoint.X;
                    lastBlackRightPoint.Y = rightPoint.Y;
                }
            } while (counter <= counterLimit);

            // Found right, now find left
            counter = 0;
            do
            {
                leftPoint.X -= 1;
                counter++;
                if (leftPoint.X < 0)
                {
                    Logger.LogToForm("Unable to find black bar at left of image... Try again...");
                    return;
                }
                if (captured.GetPixel(leftPoint.X, leftPoint.Y).ToArgb() == Color.Black.ToArgb())
                {
                    counter = 0;
                    lastBlackLeftPoint.X = leftPoint.X;
                    lastBlackLeftPoint.Y = leftPoint.Y;
                }
            } while (counter <= counterLimit);

            // Fix the stupid art...
            lastBlackLeftPoint.X += 2;

            Logger.StateToForm($"Captured coordinates for black bar - BottomY: {currentPoint.Y} RightX: {lastBlackRightPoint.X} LeftX: {lastBlackLeftPoint.X}.");

            // Error handling to verify the variables make sense
            if (lastBlackRightPoint.X - lastBlackLeftPoint.X + 1 != capturedImage.Width)
            {
                Logger.StateToForm($"ERROR: Captured coordinates don't match formula 'rightX - leftX + 1 = expectedWidth'. Try again.");
                return;
            }

            // Convert the image coordinates into screen coordinates
            Point actualTarget = new Point(cursor.X + lastBlackRightPoint.X - currentPoint.X - capturedImage.Width + 1, cursor.Y + currentPoint.Y - capturedImage.Height + 3);

            Logger.StateToForm($"Captured screen coordinates for new image top left point: {actualTarget.X}, {actualTarget.Y}.");

            captured = new Bitmap(capturedImage.Width, capturedImage.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(captured))
            {
                g.CopyFromScreen(actualTarget.X, actualTarget.Y, 0, 0, captured.Size);
            }

            capturedImage.Image = (Image)captured;
            dataImage.Image = (Image)ChoppinMinigameTool.SetImageAndCalcPoints(new Tuple<Bitmap, Point>(captured, actualTarget));
            Logger.StateToForm("Finished and displayed capture in the captured image picture box.");
        }

        #endregion
    }
}