using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace ClickRecorder
{
    public partial class Form1 : Form
    {
        private IKeyboardMouseEvents m_Events;
        private IKeyboardMouseEvents special_events;
        Point mousePos = new Point();
        List<KeyMouseAction> actions = new List<KeyMouseAction>();
        Stopwatch stopWatch = new Stopwatch();
        ReplayMachine machine;

        public Form1()
        {
            InitializeComponent();
            radioNone.Checked = true;
            //SubscribeGlobal();
            FormClosing += Main_Closing;
            special_events = Hook.GlobalEvents();
            special_events.KeyDown += OnKeyDown_ToggleReplayMachine;
        }

        private void Main_Closing(object sender, CancelEventArgs e)
        {
            Unsubscribe();
        }

        private void SubscribeGlobal()
        {
            Unsubscribe();
            Subscribe(Hook.GlobalEvents());
        }

        private void Subscribe(IKeyboardMouseEvents events)
        {
            m_Events = events;
            m_Events.KeyDown += OnKeyDown;
            m_Events.KeyUp += OnKeyUp;
            m_Events.KeyPress += HookManager_KeyPress;

            m_Events.MouseUp += OnMouseUp;
            m_Events.MouseClick += OnMouseClick;
            m_Events.MouseDoubleClick += OnMouseDoubleClick;

            m_Events.MouseMove += HookManager_MouseMove;

            //m_Events.MouseDragStarted += OnMouseDragStarted;
            //m_Events.MouseDragFinished += OnMouseDragFinished;


            m_Events.MouseDown += OnMouseDown;
        }

        private void Unsubscribe()
        {
            if (m_Events == null) return;
            m_Events.KeyDown -= OnKeyDown;
            m_Events.KeyUp -= OnKeyUp;
            m_Events.KeyPress -= HookManager_KeyPress;

            m_Events.MouseUp -= OnMouseUp;
            m_Events.MouseClick -= OnMouseClick;
            m_Events.MouseDoubleClick -= OnMouseDoubleClick;

            m_Events.MouseMove -= HookManager_MouseMove;

            //m_Events.MouseDragStarted -= OnMouseDragStarted;
            //m_Events.MouseDragFinished -= OnMouseDragFinished;

            m_Events.MouseDown -= OnMouseDown;

            m_Events.Dispose();
            m_Events = null;
        }

        private void OnKeyDown_ToggleReplayMachine(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.LMenu)
            {
                if (machine != null && machine.IsPlaying)
                {
                    machine.Stop();
                }
                else
                {
                    machine = new ReplayMachine(actions);
                    machine.Play(true);
                }
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            Log(string.Format("KeyDown  \t {0}\n", e.KeyCode));
            actions.Add(new KeyMouseAction(e.KeyCode, stopWatch.ElapsedMilliseconds, true, string.Format("KeyDown  \t {0}\n", e.KeyCode)));
            
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            Log(string.Format("KeyUp__  \t {0}\n", e.KeyCode));
            actions.Add(new KeyMouseAction(e.KeyCode, stopWatch.ElapsedMilliseconds, false, string.Format("KeyUp__  \t {0}\n", e.KeyCode)));
        }

        private void HookManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Log(string.Format("KeyPress \t {0}\n", e.KeyChar));
        }

        private void HookManager_MouseMove(object sender, MouseEventArgs e)
        {
            labelMousePosition.Text = string.Format("x={0:0000}; y={1:0000}", e.X, e.Y);
            mousePos.X = e.X;
            mousePos.Y = e.Y;
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            //Log(string.Format("MouseDown \t {0} \t {1},{2}\n", e.Button, mousePos.X, mousePos.Y));
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            //Log(string.Format("MouseUp \t {0} \t {1},{2}\n", e.Button, mousePos.X, mousePos.Y));
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            Log(string.Format("MouseClick \t {0} \t {1},{2}\n", e.Button, mousePos.X, mousePos.Y));
            actions.Add(new KeyMouseAction(e.Button, stopWatch.ElapsedMilliseconds, mousePos.X, mousePos.Y, string.Format("MouseClick \t {0} \t {1},{2}\n", e.Button, mousePos.X, mousePos.Y)));
        }

        /// <summary>
        /// Consider double click to just be another click for our purposes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Log(string.Format("MouseDoubleClick \t {0}\n", e.Button));
            Log(string.Format("MouseClick \t {0} \t {1},{2}\n", e.Button, mousePos.X, mousePos.Y));
            actions.Add(new KeyMouseAction(e.Button, stopWatch.ElapsedMilliseconds, mousePos.X, mousePos.Y, string.Format("MouseClick \t {0} \t {1},{2}\n", e.Button, mousePos.X, mousePos.Y)));
        }

        //private void OnMouseDragStarted(object sender, MouseEventArgs e)
        //{
        //    Log("MouseDragStarted\n");
        //}

        //private void OnMouseDragFinished(object sender, MouseEventArgs e)
        //{
        //    Log("MouseDragFinished\n");
        //}


        private void Log(string text)
        {
            if (IsDisposed) return;
            textBoxLog.AppendText(text);
            textBoxLog.ScrollToCaret();
        }

        private void radioGlobal_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                stopWatch.Start();
                SubscribeGlobal();
            }
        }

        private void radioNone_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                stopWatch.Stop();
                Unsubscribe();
                //Remove the click action on the tool itself
                if (textBoxLog.Text.Length > 0)
                {
                    List<string> temp = textBoxLog.Lines.ToList();
                    temp.RemoveAt(temp.Count - 1);
                    temp.RemoveAt(temp.Count - 1);
                    textBoxLog.Lines = temp.ToArray();
                }
                if (actions.Count > 0)
                {
                    actions.RemoveAt(actions.Count - 1);
                }
            }
        }

        private void clearLog_Click(object sender, EventArgs e)
        {
            textBoxLog.Clear();
            actions.Clear();
        }

        private void PlayLogButton_Click(object sender, EventArgs e)
        {
            machine = new ReplayMachine(actions);
            machine.Play();
        }

        private void PlayLogRepeatButton_Click(object sender, EventArgs e)
        {
            machine = new ReplayMachine(actions);
            machine.Play(true);
        }

        private void SaveLogButton_Click(object sender, EventArgs e)
        {
            List<string> jsonLibrary = new List<string>();
            foreach(KeyMouseAction action in actions)
            {
                jsonLibrary.Add(action.SerializeKeyMouseAction());
            }

            //write to a file
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text file|*.txt";
            sfd.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (sfd.FileName != "")
            {
                using (StreamWriter sw = new StreamWriter(sfd.OpenFile()))
                {
                    foreach(string s in jsonLibrary)
                    {
                        sw.WriteLine(s);
                    }
                }
            }
        }

        private void LoadLogButton_Click(object sender, EventArgs e)
        {
            List<string> jsonLibrary = new List<string>();

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text file|*.txt";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(ofd.OpenFile()))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        jsonLibrary.Add(line);
                    }
                }

                textBoxLog.Clear();
                actions.Clear();
                foreach (string s in jsonLibrary)
                {
                    actions.Add(new KeyMouseAction(s));
                    Log(actions[actions.Count - 1].logEntry);
                }
            }


        }
    }
}
