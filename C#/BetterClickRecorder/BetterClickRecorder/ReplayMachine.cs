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
using System.Threading;
using Gma.System.MouseKeyHook;

namespace BetterClickRecorder
{
    class ReplayMachine
    {
        List<KeyMouseAction> actions;
        Task playTask;
        CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken ct;

        public bool IsPlaying { get { return playTask.Status == TaskStatus.Running; } }

        public ReplayMachine(List<KeyMouseAction> actions)
        {
            this.actions = actions;
            ct = cts.Token;
        }

        public void Play(bool isRepeat = false)
        {
            if (actions == null)
                return;

            playTask = Task.Factory.StartNew(() =>
            {
                do
                {
                    for (int i = 0; i < actions.Count; i++)
                    {
                        if (ct.IsCancellationRequested) //Stop doing the actions if a break has been requested
                            break;
                        actions[i].PerformAction();
                        Thread.Sleep((int)actions[i].PostActionDelay);
                    }
                } while (isRepeat && !ct.IsCancellationRequested);

            }, ct);
        }

        public void Stop()
        {
            cts.Cancel();
        }
    }
}
