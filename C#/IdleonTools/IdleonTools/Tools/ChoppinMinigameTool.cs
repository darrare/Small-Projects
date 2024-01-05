using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Timer = System.Timers.Timer;
using System.Threading.Tasks;
using Gma.System.MouseKeyHook;
using System.Drawing.Imaging;

namespace IdleonTools.Tools
{
    public class ChoppinMinigameTool : Interfaces.IStoppable
    {
        #region Static Choppin Variables
        public static Bitmap CapturedImage { get; private set; }
        public static Point ScreenPoint { get; private set; }
        private static Point CenterPoint { get; set; }
        public static int[] XCoordsImagePoint { get; private set; }
        public static int[] ValidLeafColors { get; private set; } = new int[3] { Color.FromArgb(61, 161, 33).ToArgb(), Color.FromArgb(122, 206, 39).ToArgb(), Color.FromArgb(31, 89, 30).ToArgb() };
        public static int[] ValidBarColors { get; private set; } = new int[2] { Color.FromArgb(122, 206, 39).ToArgb(), Color.FromArgb(252, 255, 122).ToArgb() };
        public static int YLeafCoord { get { return 8; } }
        public static int YBarCoord { get { return 27; } }

        public static int PIXEL_PADDING { get { return 10; } }




        public static Bitmap SetImageAndCalcPoints(Tuple<Bitmap, Point> bitmapWithLocation)
        {
            CapturedImage = bitmapWithLocation.Item1;
            ScreenPoint = bitmapWithLocation.Item2;
            CenterPoint = new Point(ScreenPoint.X + CapturedImage.Width / 2, ScreenPoint.Y + CapturedImage.Height / 2);

            int halfWayPoint = CapturedImage.Height / 2;
            Bitmap dataImage = (Bitmap)CapturedImage.Clone();

            // Draw markers at 20% and multiples of 5% until 80%
            XCoordsImagePoint = new int[13] { (int)(dataImage.Width * .2f), (int)(dataImage.Width * .25f), (int)(dataImage.Width * .3f), (int)(dataImage.Width * .35f), (int)(dataImage.Width * .4f), (int)(dataImage.Width * .45f), (int)(dataImage.Width * .5f),
                                            (int)(dataImage.Width * .55f), (int)(dataImage.Width * .6f), (int)(dataImage.Width * .65f), (int)(dataImage.Width * .7f), (int)(dataImage.Width * .75f), (int)(dataImage.Width * .8f)};
            // Draw the xCoords
            foreach (int xCoord in XCoordsImagePoint)
            {
                for (int _y = dataImage.Height / 2 - 2; _y < dataImage.Height / 2 + 2; _y++)
                {
                    dataImage.SetPixel(xCoord, _y, Color.Red);
                }
            }

            // Draw the solid red bar to indicate how the leaf and bar are checked
            for (int x = XCoordsImagePoint[0]; x <= XCoordsImagePoint[XCoordsImagePoint.Length - 1]; x++)
            {
                dataImage.SetPixel(x, YLeafCoord, Color.Red);
                dataImage.SetPixel(x, YBarCoord, Color.Red);
            }

            return dataImage;
        }

        #endregion




        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point lpPoint);
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, int dx, int dy, uint cButtons, uint dwExtraInfo);
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;


        CancellationTokenSource cancellationTokenSource;
        private IKeyboardMouseEvents m_events;

        private ChoppinMinigameTool()
        {
            m_events = Hook.GlobalEvents();

            SubscribeHook();
            Logger.StateToForm("READY TO GO! Press the right alt key to start and stop.");
        }

        /// <summary>
        /// Builder pattern forces user to create tool a specific way.
        /// </summary>
        /// <returns></returns>
        public static ChoppinMinigameTool Start()
        {
            return new ChoppinMinigameTool();
        }

        private bool isRunning = false;
        private void ToggleTool(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.RMenu)
            {
                if (!isRunning)
                {
                    Logger.StateToForm("Started bot");
                    cancellationTokenSource = new CancellationTokenSource();
                    Task.Factory.StartNew(() =>
                    {
                        do
                        {
                            Thread.Sleep(25);
                            // Screencap
                            Bitmap captured = new Bitmap(CapturedImage.Width, CapturedImage.Height, PixelFormat.Format32bppArgb);
                            using (Graphics g = Graphics.FromImage(captured))
                            {
                                g.CopyFromScreen(ScreenPoint.X, ScreenPoint.Y, 0, 0, captured.Size);
                            }

                            int c = 0;
                            bool shouldClick = true;
                            foreach (int xCoord in XCoordsImagePoint)
                            {
                                c = captured.GetPixel(xCoord, YLeafCoord).ToArgb();
                                if (ValidLeafColors.Any(t => c == t))
                                {
                                    shouldClick = true;
                                    // Check to see if +- padding pixels from the xCoord are both valid colors AND CONSIDER IMMEDIATELY BELOW
                                    for (int i = xCoord - PIXEL_PADDING; i <= xCoord + PIXEL_PADDING; i+=2)
                                    {
                                        c = captured.GetPixel(i, YBarCoord).ToArgb();
                                        if (!ValidBarColors.Any(t => c == t))
                                        {
                                            shouldClick = false;
                                            break;
                                        }
                                    }
                                    if (shouldClick)
                                    {
                                        mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, CenterPoint.X, CenterPoint.Y, 0, 0);
                                        break;
                                    }
                                }
                            }
                        } while (!cancellationTokenSource.Token.IsCancellationRequested);
                    }, cancellationTokenSource.Token);
                }
                else
                {
                    cancellationTokenSource.Cancel();
                    Logger.StateToForm("Stopped bot");
                }
                isRunning = !isRunning;
            }
        }

        public void SubscribeHook()
        {
            m_events.KeyDown += ToggleTool;
        }

        public void UnsubscribeHook()
        {
            if (m_events != null)
            {
                m_events.KeyDown -= ToggleTool;
            }
        }

        public void Stop()
        {
            cancellationTokenSource.Cancel();
            UnsubscribeHook();
            Logger.LogToForm("Choppin minigame stopped and disposed...");
        }
    }
}
