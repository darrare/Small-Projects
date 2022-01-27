using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

using Gma.System.MouseKeyHook;
using System.Drawing.Imaging;

namespace SwaggerClassic
{
    public partial class SwaggerClassic : Form
    {
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        [DllImport("User32.dll")]
        static extern int ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("User32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("User32.dll")]
        public static extern void ReleaseDC(IntPtr hwnd, IntPtr dc);

        const int KEY_DOWN_EVENT = 0x0001; //Key down flag
        const int KEY_UP_EVENT = 0x0002; //Key up flag

        const int W_KEY = 0x57;
        const int A_KEY = 0x41;
        const int S_KEY = 0x53;
        const int D_KEY = 0x44;

        const int ENTER_KEY = 0x0D;

        int[] keys = new int[] { 0x57, 0x41, 0x53, 0x44 };

        bool isActive = false;
        Task timingTask;

        CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken ct;

        Process wow;

        Random rand;

        #region Tuning Variables

        int minTimeDelay = 20000;
        int maxTimeDelay = 120000;

        int minTimeToHoldButton = 200;
        int maxTimeToHoldButton = 400;

        #endregion

        private IKeyboardMouseEvents m_Events;
        Point imageCaptureLocation;
        Point mousePos;

        Timer drawRectangleTimer;

        public SwaggerClassic()
        {
            ct = cts.Token;
            rand = new Random();
            m_Events = Hook.GlobalEvents();
            drawRectangleTimer = new Timer();
            drawRectangleTimer.Interval = 100;
            drawRectangleTimer.Enabled = true;

            InitializeComponent();
        }

        private void ConsoleLog(string s)
        {
            TextBox_Log.Invoke(new MethodInvoker(delegate () 
            {
                TextBox_Log.AppendText(s + Environment.NewLine);
                TextBox_Log.ScrollToCaret();
            }));
        }

        Pen myPen = new Pen(Color.Red, 3);
        private void HookManager_MouseMove(object sender, MouseEventArgs e)
        {
            mousePos.X = e.X;
            mousePos.Y = e.Y;
        }

        private void DrawRectangle_Tick(object sender, EventArgs e)
        {
            Point topLeft = new Point(mousePos.X - PictureBox_PixelBoxOriginal.Width / 2, mousePos.Y - PictureBox_PixelBoxOriginal.Height / 2);
            g.DrawRectangle(myPen, new Rectangle(topLeft.X, topLeft.Y, PictureBox_PixelBoxOriginal.Width, PictureBox_PixelBoxOriginal.Height));
        }

        public void OnMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ConsoleLog("Section selected, displaying on picture boxes.");
                ConsoleLog("You can go afk now.");
                mousePos.X = e.X;
                mousePos.Y = e.Y;
                imageCaptureLocation = new Point(mousePos.X, mousePos.Y);

                //switch to different state;
                m_Events.MouseClick -= OnMouseClick;
                m_Events.MouseMove -= HookManager_MouseMove;
                drawRectangleTimer.Elapsed -= DrawRectangle_Tick;
                ReleaseDC(IntPtr.Zero, desktopPtr);

                Bitmap shotTook = new Bitmap(PictureBox_PixelBoxOriginal.Width, PictureBox_PixelBoxOriginal.Height, PixelFormat.Format16bppRgb565);

                using (Graphics draw = Graphics.FromImage(shotTook))
                {
                    draw.CopyFromScreen(new Point(mousePos.X - PictureBox_PixelBoxOriginal.Width / 2, mousePos.Y - PictureBox_PixelBoxOriginal.Height / 2), new Point(0, 0), PictureBox_PixelBoxOriginal.Size);
                }

                PictureBox_PixelBoxOriginal.Image = (Image)shotTook;

                cts = new CancellationTokenSource();
                ct = cts.Token;
                timingTask = Task.Factory.StartNew(TrackDifferencesBetweenImages, ct);
            }
        }

        public void TrackDifferencesBetweenImages()
        {
            while(!ct.IsCancellationRequested)
            {
                CheckQueueDisplay_Tick();
                Thread.Sleep(1500);
            }
        }

        public void CheckQueueDisplay_Tick()
        {
            Bitmap shotTook = new Bitmap(PictureBox_PixelBoxCurrent.Width, PictureBox_PixelBoxCurrent.Height, PixelFormat.Format16bppRgb565);

            using (Graphics draw = Graphics.FromImage(shotTook))
            {
                draw.CopyFromScreen(new Point(mousePos.X - PictureBox_PixelBoxCurrent.Width / 2, mousePos.Y - PictureBox_PixelBoxCurrent.Height / 2), new Point(0, 0), PictureBox_PixelBoxCurrent.Size);
            }
            
            PictureBox_PixelBoxCurrent.Image = (Image)shotTook;
            Thread.Sleep(500);
            float similarity = CalculateSimilarityBetweenBitmaps(shotTook, (Bitmap)PictureBox_PixelBoxOriginal.Image);

            Label_MatchPercentage.Invoke(new MethodInvoker(delegate () { Label_MatchPercentage.Text = "Match Percentage = " + similarity + "%"; }));
            
            if (similarity <= 33.33f)
            {
                //Start other buttons effects
                ConsoleLog("Less than 1/3 similar, pausing for 10 seconds before picking character.");
                h = wow.MainWindowHandle;
                SetForegroundWindow(h);
                Thread.Sleep(10000);
                if (ct.IsCancellationRequested) return;
                ConsoleLog("Pressing the enter key to log into character");
                keybd_event(ENTER_KEY, 0, KEY_DOWN_EVENT, 0);
                Thread.Sleep(100);
                keybd_event(ENTER_KEY, 0, KEY_UP_EVENT, 0);
                Thread.Sleep(1000);
                ConsoleLog("Starting random movement function.");
                if (ct.IsCancellationRequested) return;
                RandomMovement();
            }
        }

        private float CalculateSimilarityBetweenBitmaps(Bitmap a, Bitmap b)
        {
            if (a.Width != b.Width || a.Height != b.Height)
                return 0;

            float sum = 0;
            int count = 0;
            for (int x = 0; x < a.Width; x += 2)
            {
                for (int y = 0; y < a.Height; y += 2)
                {
                    sum += GetColorDifference(a.GetPixel(x, y), b.GetPixel(x, y));
                    count++;
                }
            }
            return 100 - ((sum / count) * 100);
        }

        /// <summary>
        /// Only care about differences larger than a total of 3 pixel colors
        /// </summary>
        private float GetColorDifference(Color a, Color b)
        {
            int difference = Math.Abs(a.R - b.R) + Math.Abs(a.G - b.G) + Math.Abs(a.B - b.B);
            return difference > 25 ? 1 : 0;
        }

        private void RandomMovement()
        {
            while (!ct.IsCancellationRequested)
            {
                byte randomKey = RandomKeySelector();
                keybd_event(randomKey, 0, KEY_DOWN_EVENT, 0);
                int time = rand.Next(minTimeToHoldButton, maxTimeToHoldButton) / 10;
                ConsoleLog("Pressing key for " + time * 10 + "ms");
                for (int i = 0; i < time; i++)
                {
                    if (ct.IsCancellationRequested)
                    {
                        break;
                    }
                    Thread.Sleep(10);
                }
                keybd_event(randomKey, 0, KEY_UP_EVENT, 0);
                if (ct.IsCancellationRequested)
                {
                    break;
                }

                time = rand.Next(minTimeDelay, maxTimeDelay) / 10;
                ConsoleLog("Waiting for " + time * 10 + "ms");
                //This for loop handles the delay between key presses
                for (int i = 0; i < rand.Next(minTimeDelay, maxTimeDelay) / 10; i++)
                {
                    if (ct.IsCancellationRequested)
                    {
                        break;
                    }
                    Thread.Sleep(10);
                }
                if (ct.IsCancellationRequested)
                {
                    break;
                }
            }
        }

        private void Button_RandomMovement_Click(object sender, EventArgs e)
        {
            isActive = !isActive;
            if (isActive)
            {
                ConsoleLog("Clicked Random Movement button");

                if (!chkBx_kevStupidProof.Checked)
                {
                    wow = Process.GetProcessesByName(txtBx_processName.Text).FirstOrDefault(); //Wow
                    if (wow == null)
                    {
                        ConsoleLog($"Unable to find process {txtBx_processName.Text}. Make sure you have the right name.");
                        return;
                    }
                    IntPtr h = wow.MainWindowHandle;
                    SetForegroundWindow(h);
                    ConsoleLog("You can go afk now. If kev stupid proof is checked, make sure to tab into the game.");
                }

       

                cts = new CancellationTokenSource();
                ct = cts.Token;
                timingTask = Task.Factory.StartNew(RandomMovement, cts.Token);
                Button_RandomMovement.Text = "STOP";
                Button_QueuePasser.Text = "STOP";
            }
            else
            {
                cts.Cancel();
                cts.Dispose();
                Button_RandomMovement.Text = "Start Random Movement";
                Button_QueuePasser.Text = "Start Queue Passer";
                ConsoleLog("--- Default state ---");
            }
        }

        private byte RandomKeySelector()
        {
            return (byte)keys[rand.Next(0, keys.Length)];
        }

        IntPtr desktopPtr;
        Graphics g;
        IntPtr h;
        private void Button_QueuePasser_Click(object sender, EventArgs e)
        {
            isActive = !isActive;
            if (isActive)
            {
                ConsoleLog("Clicked Queue Passer button");
                ConsoleLog("Right click the area you want to consider.");
                desktopPtr = GetDC(IntPtr.Zero);
                g = Graphics.FromHdc(desktopPtr);

                

                m_Events.MouseClick += OnMouseClick;
                m_Events.MouseMove += HookManager_MouseMove;
                drawRectangleTimer.Elapsed += DrawRectangle_Tick;

                wow = Process.GetProcessesByName(txtBx_processName.Text).FirstOrDefault(); //Wow
                if (wow == null)
                {
                    ConsoleLog($"Unable to find process {txtBx_processName.Text}. Make sure you have the right name.");
                    return;
                }
                h = wow.MainWindowHandle;
                SetForegroundWindow(h);


                Button_RandomMovement.Text = "STOP";
                Button_QueuePasser.Text = "STOP";
            }
            else
            {
                cts.Cancel();
                cts.Dispose();
                m_Events.MouseClick -= OnMouseClick;
                m_Events.MouseMove -= HookManager_MouseMove;
                drawRectangleTimer.Elapsed -= DrawRectangle_Tick;
                ReleaseDC(IntPtr.Zero, desktopPtr);
                Button_RandomMovement.Text = "Start Random Movement";
                Button_QueuePasser.Text = "Start Queue Passer";
                ConsoleLog("--- Default state ---");
            }
        }
    }
}
