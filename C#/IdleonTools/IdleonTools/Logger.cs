using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleonTools
{
    public static class Logger
    {
        public static TextBox? LogTextBox { get; set; }
        public static TextBox? StateTextBox { get; set; }

        public static void LogToFile(string logContent)
        {

        }

        public static void LogToForm(string logContent)
        {
            if (LogTextBox != null)
            {
                LogTextBox.Invoke(new MethodInvoker(delegate ()
                {
                    LogTextBox.AppendText(logContent + Environment.NewLine);
                    LogTextBox.ScrollToCaret();
                }));
            }
        }

        private static string previousState = "";
        public static void StateToForm(string logContent)
        {
            if (logContent != previousState && StateTextBox != null)
            {
                StateTextBox.Invoke(new MethodInvoker(delegate ()
                {
                    StateTextBox.AppendText(logContent + Environment.NewLine);
                    StateTextBox.ScrollToCaret();
                }));
            }
        }
    }
}
