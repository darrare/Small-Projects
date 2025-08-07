using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Gma.System.MouseKeyHook;

namespace IdleonTools.Tools
{
    public class FishingMinigameTool : Interfaces.IStoppable
    {
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point lpPoint);
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, int dx, int dy, uint cButtons, uint dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private Color mouseColorTrigger = Color.FromArgb(0xffffff); //TODO
        private List<Color> validTriggerColors = new List<Color>()
        {
            Color.FromArgb(255, 233, 0, 0),
            Color.FromArgb(255, 255, 112, 119),
            Color.FromArgb(255, 255, 171, 143),
            //Color.FromArgb(255, 255, 255, 255) // White for debugging
        };

        private IKeyboardMouseEvents m_events;

        CancellationTokenSource cancellationTokenSource;

        private FishingMinigameTool()
        {
            cancellationTokenSource = new CancellationTokenSource();
            m_events = Hook.GlobalEvents();

            SubscribeHook();
            Logger.StateToForm("Awaiting mouse down...");

        }

        /// <summary>
        /// Builder pattern forces user to create tool a specific way.
        /// </summary>
        /// <returns></returns>
        public static FishingMinigameTool Start()
        {
            return new FishingMinigameTool();
        }

        private void SubscribeHook()
        {
            m_events.MouseDown += MouseDown;
            m_events.MouseUp += MouseUp;
        }

        private void MouseUp(object sender, MouseEventArgs e)
        {
            //cancellationTokenSource.Cancel();
        }

        private async void MouseDown(object sender, MouseEventArgs e)
        {
            UnsubscribeHook();
            try
            {
                await Task.Factory.StartNew(() =>
                {
                    //Logger.StateToForm("Mouse down, awaiting color...");
                    Point cursor = new Point();
                    Color result = Color.Black;
                    //var watch = System.Diagnostics.Stopwatch.StartNew();
                    //watch.Start();
                    do
                    {
                        GetCursorPos(ref cursor);
                        //long time = watch.ElapsedMilliseconds;
                        result = GetColorAt(cursor);
                        //Logger.LogToForm((watch.ElapsedMilliseconds - time).ToString());
                        //Logger.LogToForm(watch.ElapsedMilliseconds.ToString());

                        if (cancellationTokenSource.IsCancellationRequested)
                        {
                            return;
                        }
                    } while (validTriggerColors.All(t => !areColorsIdenticalRGB(t, result))) ;

                    //Logger.StateToForm("Color found, clicking...");
                    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, cursor.X, cursor.Y, 0, 0);
                    //Logger.StateToForm("Awaiting mouse down...");
                }, cancellationTokenSource.Token);
            }
            catch (Exception)
            {

            }

            SubscribeHook();
        }

        private bool areColorsIdenticalRGB(Color a, Color b)
        {
            return a.R == b.R && a.G == b.G && a.B == b.B;
        }

        Bitmap screenPixel = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
        private Color GetColorAt(Point location)
        {
            using (Graphics gdest = Graphics.FromImage(screenPixel))
            {
                using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
                {
                    IntPtr hSrcDC = gsrc.GetHdc();
                    IntPtr hDC = gdest.GetHdc();
                    int retval = BitBlt(hDC, 0, 0, 1, 1, hSrcDC, location.X, location.Y, (int)CopyPixelOperation.SourceCopy);
                    gdest.ReleaseHdc();
                    gsrc.ReleaseHdc();
                }
            }

            return screenPixel.GetPixel(0, 0);
        }

        private void UnsubscribeHook()
        {
            m_events.MouseDown -= MouseDown;
            m_events.MouseUp -= MouseUp;
        }

        public void Stop()
        {
            cancellationTokenSource.Cancel();
            UnsubscribeHook();
            Logger.LogToForm("Fishing minigame stopped and disposed...");
        }
    }
}
