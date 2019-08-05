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

namespace ClickRecorder
{
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

        Keys keyCode;
        MouseButtons mouseButton;
        bool isDown;
        int x, y;
        public long time;
        public string logEntry;

        delegate void DoAction();
        DoAction doAction;

        public KeyMouseAction(Keys keyCode, long time, bool isDown, string logEntry)
        {
            this.keyCode = keyCode;
            this.time = time;
            this.logEntry = logEntry;
            this.isDown = isDown;

            doAction = () =>
            {
                keybd_event((byte)this.keyCode, 0, (isDown ? KEY_DOWN_EVENT : KEY_UP_EVENT), 0);
            };
        }

        public KeyMouseAction(MouseButtons mouseButton, long time, int x, int y, string logEntry)
        {
            this.mouseButton = mouseButton;
            this.x = x;
            this.y = y;
            this.time = time;
            this.logEntry = logEntry;

            if (mouseButton == MouseButtons.Left)
            {
                doAction = () =>
                {
                    SetCursorPos(this.x, this.y);
                    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, this.x, this.y, 0, 0);
                };
            }
            else if (mouseButton == MouseButtons.Right)
            {
                doAction = () =>
                {
                    SetCursorPos(this.x, this.y);
                    mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, this.x, this.y, 0, 0);
                };
            }
            else if (mouseButton == MouseButtons.Middle)
            {
                //Not implemented
            }
        }

        /// <summary>
        /// Deserializing constructor
        /// </summary>
        /// <param name="serialized">Serialized string</param>
        public KeyMouseAction(string serialized)
        {
            var result = JsonConvert.DeserializeAnonymousType(serialized, new
            {
                Type = "",
                KeyCode = (int)5,
                MouseButton = (int)5,
                IsDown = (bool)isDown,
                X = (int)5,
                Y = (int)5,
                Time = (long)5,
                LogEntry = ""
            });

            if (result.Type == "Key")
            {
                keyCode = (Keys)result.KeyCode;
                doAction = () =>
                {
                    keybd_event((byte)this.keyCode, 0, (result.IsDown ? KEY_DOWN_EVENT : KEY_UP_EVENT), 0);
                };
            }
            else
            {
                mouseButton = (MouseButtons)result.MouseButton;
                if (mouseButton == MouseButtons.Left)
                {
                    doAction = () =>
                    {
                        SetCursorPos(this.x, this.y);
                        mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, this.x, this.y, 0, 0);
                    };
                }
                else if (mouseButton == MouseButtons.Right)
                {
                    doAction = () =>
                    {
                        SetCursorPos(this.x, this.y);
                        mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, this.x, this.y, 0, 0);
                    };
                }
                else if (mouseButton == MouseButtons.Middle)
                {
                    //Not implemented
                }
            }
                

            x = result.X;
            y = result.Y;
            time = result.Time;
            logEntry = result.LogEntry;
        }

        public string SerializeKeyMouseAction()
        {
            return JsonConvert.SerializeObject(new
            {
                Type = (keyCode != 0 ? "Key" : "Mouse"),
                KeyCode = (int)keyCode,
                MouseButton = (int)mouseButton,
                IsDown = isDown,
                X = x,
                Y = y,
                Time = time,
                LogEntry = logEntry
            });
        }

        public void PerformAction()
        {
            doAction();
        }
    }
}
