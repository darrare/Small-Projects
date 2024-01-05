using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using Newtonsoft.Json;

namespace BetterClickRecorder
{
    public enum KeyMouseEnum
    {
        Key,
        Mouse
    }

    public class KeyMouseAction
    {
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, int dx, int dy, uint cButtons, uint dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, uint dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        const int KEY_DOWN_EVENT = 0x0001; //Key down flag
        const int KEY_UP_EVENT = 0x0002; //Key up flag


        #region Json Attributes
        public KeyMouseEnum Type { get { return ((int)KeyCode != 0 ? KeyMouseEnum.Key : KeyMouseEnum.Mouse); } }
        public Keys KeyCode { get; set; } = 0;
        public bool IsDown { get; set; }
        public MouseButtons MouseButton { get; set; } = 0;
        public int X { get; set; }
        public int Y { get; set; }
        public long PostActionDelay { get; set; }
        public string Comment { get; set; }
        #endregion

        delegate void DoAction();
        DoAction doAction;

        public KeyMouseAction()
        {

        }

        public KeyMouseAction(Keys keyCode, bool isDown, string comment = "")
        {
            this.KeyCode = keyCode;
            this.IsDown = isDown;
            this.Comment = comment;

            doAction = () =>
            {
                keybd_event((byte)this.KeyCode, 0, (isDown ? 0 : KEY_UP_EVENT), 0);
            };
        }

        public KeyMouseAction(MouseButtons mouseButton, int x, int y, string comment = "")
        {
            this.MouseButton = mouseButton;
            this.X = x;
            this.Y = y;
            this.Comment = comment;

            if (mouseButton == MouseButtons.Left)
            {
                doAction = () =>
                {
                    SetCursorPos(this.X, this.Y);
                    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, this.X, this.Y, 0, 0);
                };
            }
            else if (mouseButton == MouseButtons.Right)
            {
                doAction = () =>
                {
                    SetCursorPos(this.X, this.Y);
                    mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, this.X, this.Y, 0, 0);
                };
            }
            else if (mouseButton == MouseButtons.Middle)
            {
                //Not implemented
            }
        }

        public string SerializeKeyMouseAction()
        {
            return JsonConvert.SerializeObject(this);
        }

        public void PerformAction()
        {
            if (doAction == null)
            {
                if (this.Type == KeyMouseEnum.Key)
                {
                    doAction = () =>
                    {
                        keybd_event((byte)this.KeyCode, 0, (IsDown ? 0 : KEY_UP_EVENT), 0);
                    };
                }
                else
                {
                    doAction = () =>
                    {
                        SetCursorPos(this.X, this.Y);
                        mouse_event(this.MouseButton == MouseButtons.Left ?
                            (uint)(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP) :
                            (uint)(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP), this.X, this.Y, 0, 0);
                    };

                    // Mouse button down currently not implemented
                }
            }
            doAction();
        }
    }
}
