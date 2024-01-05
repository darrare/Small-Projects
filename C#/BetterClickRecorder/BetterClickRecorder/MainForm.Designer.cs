namespace BetterClickRecorder
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.buttonStartRecording = new System.Windows.Forms.Button();
            this.buttonRunRepeat = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonRunOnce = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.labelMousePosition = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonSetAllDelaay = new System.Windows.Forms.Button();
            this.textBoxSetAllDelay = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(12, 70);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 25;
            this.dataGridView.Size = new System.Drawing.Size(781, 449);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.DataMemberChanged += new System.EventHandler(this.datagridView_DataChanged);
            // 
            // buttonStartRecording
            // 
            this.buttonStartRecording.Location = new System.Drawing.Point(12, 13);
            this.buttonStartRecording.Name = "buttonStartRecording";
            this.buttonStartRecording.Size = new System.Drawing.Size(137, 23);
            this.buttonStartRecording.TabIndex = 1;
            this.buttonStartRecording.Text = "START RECORDING";
            this.buttonStartRecording.UseVisualStyleBackColor = true;
            this.buttonStartRecording.Click += new System.EventHandler(this.buttonStartRecording_Click);
            // 
            // buttonRunRepeat
            // 
            this.buttonRunRepeat.Location = new System.Drawing.Point(551, 12);
            this.buttonRunRepeat.Name = "buttonRunRepeat";
            this.buttonRunRepeat.Size = new System.Drawing.Size(75, 23);
            this.buttonRunRepeat.TabIndex = 3;
            this.buttonRunRepeat.Text = "Run Repeat";
            this.buttonRunRepeat.UseVisualStyleBackColor = true;
            this.buttonRunRepeat.Click += new System.EventHandler(this.buttonRunRepeat_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(713, 12);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonRunOnce
            // 
            this.buttonRunOnce.Location = new System.Drawing.Point(632, 12);
            this.buttonRunOnce.Name = "buttonRunOnce";
            this.buttonRunOnce.Size = new System.Drawing.Size(75, 23);
            this.buttonRunOnce.TabIndex = 5;
            this.buttonRunOnce.Text = "Run Once";
            this.buttonRunOnce.UseVisualStyleBackColor = true;
            this.buttonRunOnce.Click += new System.EventHandler(this.buttonRunOnce_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(713, 41);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(75, 23);
            this.buttonLoad.TabIndex = 6;
            this.buttonLoad.Text = "Load";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(632, 41);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 7;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // labelMousePosition
            // 
            this.labelMousePosition.AutoSize = true;
            this.labelMousePosition.Location = new System.Drawing.Point(12, 45);
            this.labelMousePosition.Name = "labelMousePosition";
            this.labelMousePosition.Size = new System.Drawing.Size(129, 15);
            this.labelMousePosition.TabIndex = 9;
            this.labelMousePosition.Text = "X={0:####};y={1:####}";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(354, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(191, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "Press Right Alt to toggle run repeat";
            // 
            // buttonSetAllDelaay
            // 
            this.buttonSetAllDelaay.Location = new System.Drawing.Point(354, 41);
            this.buttonSetAllDelaay.Name = "buttonSetAllDelaay";
            this.buttonSetAllDelaay.Size = new System.Drawing.Size(87, 23);
            this.buttonSetAllDelaay.TabIndex = 11;
            this.buttonSetAllDelaay.Text = "Set All Delay";
            this.buttonSetAllDelaay.UseVisualStyleBackColor = true;
            this.buttonSetAllDelaay.Click += new System.EventHandler(this.buttonSetAllDelaay_Click);
            // 
            // textBoxSetAllDelay
            // 
            this.textBoxSetAllDelay.Location = new System.Drawing.Point(447, 41);
            this.textBoxSetAllDelay.Name = "textBoxSetAllDelay";
            this.textBoxSetAllDelay.Size = new System.Drawing.Size(100, 23);
            this.textBoxSetAllDelay.TabIndex = 12;
            this.textBoxSetAllDelay.Text = "50";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 531);
            this.Controls.Add(this.textBoxSetAllDelay);
            this.Controls.Add(this.buttonSetAllDelaay);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelMousePosition);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.buttonRunOnce);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonRunRepeat);
            this.Controls.Add(this.buttonStartRecording);
            this.Controls.Add(this.dataGridView);
            this.Name = "MainForm";
            this.Text = "Click Recorder";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView dataGridView;
        private Button buttonStartRecording;
        private Button buttonRunRepeat;
        private Button buttonSave;
        private Button buttonRunOnce;
        private Button buttonLoad;
        private Button buttonClear;
        private Label labelMousePosition;
        private Label label3;
        private Button buttonSetAllDelaay;
        private TextBox textBoxSetAllDelay;
    }
}