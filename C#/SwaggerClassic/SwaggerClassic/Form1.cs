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

namespace SwaggerClassic
{
    public partial class Form1 : Form
    {
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        [DllImport("User32.dll")]
        static extern int ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        const int KEY_DOWN_EVENT = 0x0001; //Key down flag
        const int KEY_UP_EVENT = 0x0002; //Key up flag

        const int W_KEY = 0x57;
        const int A_KEY = 0x41;
        const int S_KEY = 0x53;
        const int D_KEY = 0x44;

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

        public Form1()
        {
            ct = cts.Token;
            rand = new Random();
            InitializeComponent();
        }

        private void Button_Toggle_Click(object sender, EventArgs e)
        {
            isActive = !isActive;
            if (isActive)
            {
                wow = Process.GetProcessesByName("Wow").FirstOrDefault(); //Wow
                IntPtr h = wow.MainWindowHandle;
                SetForegroundWindow(h);
                cts = new CancellationTokenSource();
                ct = cts.Token;
                timingTask = Task.Factory.StartNew(() =>
                {
                    while(!ct.IsCancellationRequested)
                    {
                        byte randomKey = RandomKeySelector();
                        keybd_event(randomKey, 0, KEY_DOWN_EVENT, 0);
                        for (int i = 0; i < rand.Next(minTimeToHoldButton, maxTimeToHoldButton) / 10; i++)
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
                }, cts.Token);
                Button_Toggle.Text = "STOP";
            }
            else
            {
                cts.Cancel();
                cts.Dispose();
                Button_Toggle.Text = "Start";
            }
        }

        private byte RandomKeySelector()
        {
            return (byte)keys[rand.Next(0, keys.Length)];
        }
    }
}
