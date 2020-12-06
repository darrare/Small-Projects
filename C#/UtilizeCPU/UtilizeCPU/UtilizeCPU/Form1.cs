using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UtilizeCPU
{
    public partial class UtilizeCPU : Form
    {
        private const int NUM_THREADS_TO_CREATE = 12;
        private const int SIZE_OF_LIST_TO_BOGO_SORT = 10000;

        private List<Task> tasks = new List<Task>();
        private CancellationTokenSource cancellationSource = new CancellationTokenSource();
        private CancellationToken cancellationToken;
        public UtilizeCPU()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ConsoleLog($"Starting {NUM_THREADS_TO_CREATE} threads.");
            cancellationToken = cancellationSource.Token;
            for (int i = 0; i < NUM_THREADS_TO_CREATE; i++)
            {
                int x = i;
                tasks.Add(Task.Factory.StartNew(() => BogoSort(x, cancellationToken), cancellationToken));
            }
            ConsoleLog($"All threads created.");
        }

        private void BogoSort(int threadNumber, CancellationToken cancellationToken)
        {
            List<int> toSort = new List<int>();
            Random rand = new Random();
            ConsoleLog($"Bogo sorting on thread {threadNumber}");

            for (int i = 0; i < SIZE_OF_LIST_TO_BOGO_SORT; i++)
            {
                toSort.Add(SIZE_OF_LIST_TO_BOGO_SORT - i);
            }

            while (!toSort.IsSorted() && !cancellationToken.IsCancellationRequested)
            {
                List<int> newList = new List<int>();
                for (int i = toSort.Count - 1; i >= 0; i--)
                {
                    int index = rand.Next(0, i);
                    newList.Add(toSort.ElementAt(index));
                    toSort.RemoveAt(index);
                }
                toSort = newList;
            }
            ConsoleLog($"Thread {threadNumber} cancelled.");
        }

        private void ConsoleLog(string s)
        {
            txtBoxLogMessage.Invoke(new MethodInvoker(delegate ()
            {
                txtBoxLogMessage.AppendText(s + Environment.NewLine);
                txtBoxLogMessage.ScrollToCaret();
            }));
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            cancellationSource.Cancel();
            ConsoleLog($"Informing all threads to stop");
        }
    }

    public static class ExtensionMethods
    {
        public static bool IsSorted(this List<int> collection)
        {
            for (int i = 1; i < collection.Count; i++)
            {
                if (collection[i] < collection[i - 1])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
