using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PROtools
{
    public class BotMachine
    {
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, uint dwExtraInfo);
        const int KEY_DOWN_EVENT = 0x0001; //Key down flag
        const int KEY_UP_EVENT = 0x0002; //Key up flag

        Random rand = new Random();

        CancellationTokenSource tokenSource2 = new CancellationTokenSource();
        CancellationToken ct;

        Task task = null;
        bool directionToggle = true;
        /// <summary>
        /// useMoves disabled and hard coded to just use the first move due to timing issues I don't want to solve.
        /// It's possible for the menu options to enable part way through the "In Battle" logic which would cause a desync
        /// and you might run, use an item, etc. So just keeping it on 1 for now. It's safer this way anyways
        /// </summary>
        /// <param name="pixelColor"></param>
        /// <param name="pixelPoint"></param>
        /// <param name="isLeftRight"></param>
        /// <param name="movementTimeMs"></param>
        /// <param name="movementTimeVarianceMs"></param>
        /// <param name="useMoves"></param>
        public BotMachine(Color pixelColor, Point pixelPoint, bool isLeftRight, int movementTimeMs, int movementTimeVarianceMs, bool[] useMoves)
        {
            ct = tokenSource2.Token;

            task = Task.Factory.StartNew(() =>
            {
                while(true)
                {
                    if (ct.IsCancellationRequested) return;

                    if (AreColorsSimilar(Utils.GetColorAt(pixelPoint), Form1.Instance.pixelColor))
                    {
                        Form1.Instance.SetStatus("In Battle...");
                        keybd_event((byte)Keys.D1, 0, 0, 0);
                        Thread.Sleep(40 + GetRandomVariance(20));
                        keybd_event((byte)Keys.D1, 0, KEY_UP_EVENT, 0);
                        Thread.Sleep(150 + GetRandomVariance(40));

                        List<Keys> possibleKeyOptions = new List<Keys>();
                        if (useMoves[0]) possibleKeyOptions.Add(Keys.D1);
                        if (useMoves[1]) possibleKeyOptions.Add(Keys.D2);
                        if (useMoves[2]) possibleKeyOptions.Add(Keys.D3);
                        if (useMoves[3]) possibleKeyOptions.Add(Keys.D4);

                        Keys selectedKey = possibleKeyOptions[rand.Next(0, possibleKeyOptions.Count)];
                        keybd_event((byte)selectedKey, 0, 0, 0);
                        Thread.Sleep(40 + GetRandomVariance(20));
                        keybd_event((byte)selectedKey, 0, KEY_UP_EVENT, 0);
                        Thread.Sleep(movementTimeMs / 5 + GetRandomVariance(movementTimeMs / 10));
                    }
                    else
                    {
                        Form1.Instance.SetStatus("In Overworld...");
                        if (isLeftRight)
                        {
                            if (directionToggle)
                            {
                                keybd_event((byte)Keys.A, 0, 0, 0);
                            }
                            else
                            {
                                keybd_event((byte)Keys.D, 0, 0, 0);
                            }
                        }
                        else
                        {
                            if (directionToggle)
                            {
                                keybd_event((byte)Keys.W, 0, 0, 0);
                            }
                            else
                            {
                                keybd_event((byte)Keys.S, 0, 0, 0);
                            }
                        }
                        Thread.Sleep(movementTimeMs + GetRandomVariance(movementTimeVarianceMs));
                        if (isLeftRight)
                        {
                            if (directionToggle)
                            {
                                keybd_event((byte)Keys.A, 0, KEY_UP_EVENT, 0);
                            }
                            else
                            {
                                keybd_event((byte)Keys.D, 0, KEY_UP_EVENT, 0);
                            }
                        }
                        else
                        {
                            if (directionToggle)
                            {
                                keybd_event((byte)Keys.W, 0, KEY_UP_EVENT, 0);
                            }
                            else
                            {
                                keybd_event((byte)Keys.S, 0, KEY_UP_EVENT, 0);
                            }
                        }
                        directionToggle = !directionToggle;
                    }
                }
            }, ct);
        }

        private bool AreColorsSimilar(Color a, Color b)
        {
            int difference = Math.Abs(a.R - b.R) + Math.Abs(a.G - b.G) + Math.Abs(a.B - b.B);
            return difference <= 25;
        }

        private int GetRandomVariance(int plusMinus)
        {
            return rand.Next(-plusMinus, plusMinus + 1);
        }

        public async void Destroy()
        {
            tokenSource2.Cancel();
            await task;
            task.Dispose();
        }
    }
}
