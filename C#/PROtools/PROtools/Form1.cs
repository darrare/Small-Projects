using Gma.System.MouseKeyHook;
using Microsoft.VisualBasic.Devices;

namespace PROtools
{
    public partial class Form1 : Form
    {
        private bool isStarted = false;
        private BotMachine botMachine;
        public Color pixelColor;
        private Point pixelPoint;

        private IKeyboardMouseEvents m_globalHook;

        private static Form1 instance;
        public static Form1 Instance { get { return instance; } }

        public Form1()
        {
            instance = this;
            InitializeComponent();
            Subscribe();
            radioButton_leftRight.CheckedChanged += handleRadioButtons;

            radioButton_upDown.CheckedChanged += handleRadioButtons;
        }

        public void Subscribe()
        {
            m_globalHook = Hook.GlobalEvents();

            m_globalHook.KeyUp += Task_ListenForHotKeys;
        }

        public void Unsubscribe()
        {
            m_globalHook.KeyUp -= Task_ListenForHotKeys;

            //It is recommened to dispose it
            m_globalHook.Dispose();
        }

        public void Task_ListenForHotKeys(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    button_setBattlePixel_Click(null, null);
                    break;
                case Keys.F6:
                    button_startStop_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        private void handleRadioButtons(object sender, EventArgs e)
        {
            RadioButton senderRadio = (RadioButton)sender;
            if (!senderRadio.Checked)
            {
                return;
            }

            if (senderRadio == radioButton_leftRight)
            {
                radioButton_upDown.Checked = false;
            }
            else
            {
                radioButton_leftRight.Checked = false;
            }
        }

        private void button_setBattlePixel_Click(object sender, EventArgs e)
        {
            Tuple<Color, Point> pixel = Utils.GetColorAtMousePosition();
            pixelColor = pixel.Item1;
            pixelPoint = pixel.Item2;
            int width = pictureBox_battlePixel.Width;
            int height = pictureBox_battlePixel.Height;

            Bitmap bt = new Bitmap(width, height);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    bt.SetPixel(x, y, pixelColor);
                }
            }
            pictureBox_battlePixel.Image = bt;
            pictureBox_battlePixel.Refresh();
            label_r.Text = pixelColor.R.ToString();
            label_g.Text = pixelColor.G.ToString();
            label_b.Text = pixelColor.B.ToString();
        }

        private void button_startStop_Click(object sender, EventArgs e)
        {
            if (isStarted)
            {
                isStarted = false;
                botMachine.Destroy();
                botMachine = null;
            }
            else
            {
                int movementTime = StringToInt(textBox_movementTime.Text);
                int movementTimeVariance = StringToInt(textBox_movementTimeVariance.Text);

                if (movementTime == -1 || movementTimeVariance == -1)
                {
                    LogMessage("Bot not started...");
                    return;
                }

                botMachine = new BotMachine(
                    pixelColor, 
                    pixelPoint, 
                    radioButton_leftRight.Checked,
                    movementTime,
                    movementTimeVariance, 
                    new bool[4] { checkBox_move1.Checked, checkBox_move2.Checked, checkBox_move3.Checked, checkBox_move4.Checked });
                isStarted = true;
            }
        }

        private int StringToInt(string str)
        {
            if (int.TryParse(str, out int result))
            {
                return result;
            }
            LogMessage($"Failure converting {str} to int...");
            return -1;
        }

        public void LogMessage(string message)
        {
            textBox_notifications.Invoke(new MethodInvoker(delegate ()
            {
                textBox_notifications.AppendText(textBox_notifications + Environment.NewLine);
                textBox_notifications.ScrollToCaret();
            }));
        }

        public void SetStatus(string message)
        {
            label_status.Invoke(new MethodInvoker(delegate ()
            {
                label_status.Text = message;
            }));
        }
    }
}