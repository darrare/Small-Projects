using Gma.System.MouseKeyHook;
using Newtonsoft.Json;
using System.Data;

namespace BetterClickRecorder
{
    public partial class MainForm : Form
    {
        private static MainForm instance;
        public static MainForm Instance { get { return instance; } }

        public List<KeyMouseAction> Actions { get; set; } = new List<KeyMouseAction>();

        ReplayMachine machine;

        private IKeyboardMouseEvents toggle;


        public MainForm()
        {
            instance = this;
            toggle = Hook.GlobalEvents();
            toggle.KeyDown += CheckForRunRepeatToggle;
            InitializeComponent();
        }

        public void CheckForRunRepeatToggle(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.RMenu)
            {
                if (machine != null && machine.IsPlaying)
                {
                    machine.Stop();
                }
                else
                {
                    machine = new ReplayMachine(Actions);
                    machine.Play(true);
                }
            }
        }

        public void UpdateMousePos(Point mousePos)
        {
            labelMousePosition.Text = string.Format("x={0:0000}; y={1:0000}", mousePos.X, mousePos.Y);
        }

        public void Log(string message)
        {
            // Who needs logging?
        }

        Recorder rec;
        Color backColor;
        private void buttonStartRecording_Click(object sender, EventArgs e)
        {
            if (rec == null)
            {
                rec = Recorder.Instantiate();
                buttonStartRecording.Text = "RECORDING";
                backColor = buttonStartRecording.BackColor;
                buttonStartRecording.BackColor = Color.Red;
            }
            else
            {
                buttonStartRecording.BackColor = backColor;
                buttonStartRecording.Text = "START RECORDING";
                // TODO: Store the dataset here. Remember to take off the last click
                Actions = rec.actions.Take(rec.actions.Count - 1).ToList();
                if (Actions.Any()) Actions.Last().PostActionDelay = 0;
                dataGridView.DataSource = Actions;
                dataGridView.AutoResizeColumns();
                rec = null;
            }
        }

        private void buttonRunRepeat_Click(object sender, EventArgs e)
        {
            machine = new ReplayMachine(Actions);
            machine.Play(true);
        }

        private void buttonRunOnce_Click(object sender, EventArgs e)
        {
            machine = new ReplayMachine(Actions);
            machine.Play();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            dataGridView.DataSource = null;
            Actions = new List<KeyMouseAction>();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            //write to a file
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Json file|*.json";
            sfd.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (sfd.FileName != "")
            {
                using (StreamWriter sw = new StreamWriter(sfd.OpenFile()))
                {
                    sw.Write(JsonConvert.SerializeObject((List<KeyMouseAction>)dataGridView.DataSource));
                }
            }
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Json file|*.json";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(ofd.OpenFile()))
                {
                    Actions = JsonConvert.DeserializeObject<List<KeyMouseAction>>(sr.ReadToEnd());
                }

                dataGridView.DataSource = Actions;
                dataGridView.AutoResizeColumns();
            }
        }

        private void datagridView_DataChanged(object sender, EventArgs e)
        {
            Actions = (List<KeyMouseAction>)dataGridView.DataSource;
        }

        private void buttonSetAllDelaay_Click(object sender, EventArgs e)
        {
            if(int.TryParse(textBoxSetAllDelay.Text, out int result))
            {
                Actions.ForEach(t => t.PostActionDelay = result);
            }
            dataGridView.DataSource = Actions;
            dataGridView.Refresh();
            dataGridView.AutoResizeColumns();
        }
    }
}