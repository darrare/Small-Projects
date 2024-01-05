using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterClickRecorder
{
    class Recorder
    {
        public List<KeyMouseAction> actions = new List<KeyMouseAction>();
        private IKeyboardMouseEvents m_Events;
        Stopwatch stopWatch;
        Point mousePos = new Point();
        long prevTime = 0;
        private Recorder()
        {
            m_Events = Hook.GlobalEvents();
            m_Events.KeyDown += OnKeyDown;
            m_Events.KeyUp += OnKeyUp;
            m_Events.KeyPress += HookManager_KeyPress;

            m_Events.MouseUp += OnMouseUp;
            m_Events.MouseClick += OnMouseClick;
            m_Events.MouseDoubleClick += OnMouseDoubleClick;

            m_Events.MouseMove += HookManager_MouseMove;

            stopWatch = new Stopwatch();
            stopWatch.Start();
        }

        public static Recorder Instantiate()
        {
            return new Recorder();
        }

        private long getTime()
        {
            if (prevTime == 0)
            {
                prevTime = stopWatch.ElapsedMilliseconds;
                return 0;
            }
            else
            {
                long result = stopWatch.ElapsedMilliseconds - prevTime;
                prevTime = stopWatch.ElapsedMilliseconds;
                return result;
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            //Log(string.Format("KeyDown  \t {0}\n", e.KeyCode));
            if (actions.Count != 0) actions.Last().PostActionDelay = stopWatch.ElapsedMilliseconds - prevTime;
            actions.Add(new KeyMouseAction(e.KeyCode, true));
            prevTime = stopWatch.ElapsedMilliseconds;
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            // Log(string.Format("KeyUp__  \t {0}\n", e.KeyCode));
            if (actions.Count != 0) actions.Last().PostActionDelay = stopWatch.ElapsedMilliseconds - prevTime;
            actions.Add(new KeyMouseAction(e.KeyCode, false));
            prevTime = stopWatch.ElapsedMilliseconds;
        }

        private void HookManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Log(string.Format("KeyPress \t {0}\n", e.KeyChar));
        }

        private void HookManager_MouseMove(object sender, MouseEventArgs e)
        {
            mousePos.X = e.X;
            mousePos.Y = e.Y;
            MainForm.Instance.UpdateMousePos(mousePos);
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
            //Log(string.Format("MouseClick \t {0} \t {1},{2}\n", e.Button, mousePos.X, mousePos.Y));
            if (actions.Count != 0) actions.Last().PostActionDelay = stopWatch.ElapsedMilliseconds - prevTime;
            actions.Add(new KeyMouseAction(e.Button, mousePos.X, mousePos.Y));
            prevTime = stopWatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Consider double click to just be another click for our purposes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Log(string.Format("MouseDoubleClick \t {0}\n", e.Button));
            //Log(string.Format("MouseClick \t {0} \t {1},{2}\n", e.Button, mousePos.X, mousePos.Y));
            if (actions.Count != 0) actions.Last().PostActionDelay = stopWatch.ElapsedMilliseconds - prevTime;
            actions.Add(new KeyMouseAction(e.Button, mousePos.X, mousePos.Y));
            prevTime = stopWatch.ElapsedMilliseconds;
        }

        //private void OnMouseDragStarted(object sender, MouseEventArgs e)
        //{
        //    Log("MouseDragStarted\n");
        //}

        //private void OnMouseDragFinished(object sender, MouseEventArgs e)
        //{
        //    Log("MouseDragFinished\n");
        //}
    }
}

