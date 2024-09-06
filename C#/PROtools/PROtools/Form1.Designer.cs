namespace PROtools
{
    partial class Form1
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
            this.radioButton_leftRight = new System.Windows.Forms.RadioButton();
            this.radioButton_upDown = new System.Windows.Forms.RadioButton();
            this.textBox_movementTime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_movementTimeVariance = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox_move1 = new System.Windows.Forms.CheckBox();
            this.checkBox_move2 = new System.Windows.Forms.CheckBox();
            this.checkBox_move3 = new System.Windows.Forms.CheckBox();
            this.checkBox_move4 = new System.Windows.Forms.CheckBox();
            this.button_setBattlePixel = new System.Windows.Forms.Button();
            this.button_startStop = new System.Windows.Forms.Button();
            this.textBox_notifications = new System.Windows.Forms.RichTextBox();
            this.pictureBox_battlePixel = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label_status = new System.Windows.Forms.Label();
            this.label_r = new System.Windows.Forms.Label();
            this.label_g = new System.Windows.Forms.Label();
            this.label_b = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_battlePixel)).BeginInit();
            this.SuspendLayout();
            // 
            // radioButton_leftRight
            // 
            this.radioButton_leftRight.AutoSize = true;
            this.radioButton_leftRight.Checked = true;
            this.radioButton_leftRight.Location = new System.Drawing.Point(17, 12);
            this.radioButton_leftRight.Name = "radioButton_leftRight";
            this.radioButton_leftRight.Size = new System.Drawing.Size(78, 19);
            this.radioButton_leftRight.TabIndex = 0;
            this.radioButton_leftRight.TabStop = true;
            this.radioButton_leftRight.Text = "Left/Right";
            this.radioButton_leftRight.UseVisualStyleBackColor = true;
            // 
            // radioButton_upDown
            // 
            this.radioButton_upDown.AutoSize = true;
            this.radioButton_upDown.Location = new System.Drawing.Point(17, 37);
            this.radioButton_upDown.Name = "radioButton_upDown";
            this.radioButton_upDown.Size = new System.Drawing.Size(76, 19);
            this.radioButton_upDown.TabIndex = 1;
            this.radioButton_upDown.Text = "Up/Down";
            this.radioButton_upDown.UseVisualStyleBackColor = true;
            // 
            // textBox_movementTime
            // 
            this.textBox_movementTime.Location = new System.Drawing.Point(123, 11);
            this.textBox_movementTime.Name = "textBox_movementTime";
            this.textBox_movementTime.Size = new System.Drawing.Size(100, 23);
            this.textBox_movementTime.TabIndex = 2;
            this.textBox_movementTime.Text = "300";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(229, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Movement Time (ms)";
            // 
            // textBox_movementTimeVariance
            // 
            this.textBox_movementTimeVariance.Location = new System.Drawing.Point(123, 36);
            this.textBox_movementTimeVariance.Name = "textBox_movementTimeVariance";
            this.textBox_movementTimeVariance.Size = new System.Drawing.Size(100, 23);
            this.textBox_movementTimeVariance.TabIndex = 4;
            this.textBox_movementTimeVariance.Text = "100";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(229, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Movement Time Variance (ms +-)";
            // 
            // checkBox_move1
            // 
            this.checkBox_move1.AutoSize = true;
            this.checkBox_move1.Checked = true;
            this.checkBox_move1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_move1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.checkBox_move1.Location = new System.Drawing.Point(17, 79);
            this.checkBox_move1.Name = "checkBox_move1";
            this.checkBox_move1.Size = new System.Drawing.Size(87, 19);
            this.checkBox_move1.TabIndex = 6;
            this.checkBox_move1.Text = "Use Move 1";
            this.checkBox_move1.UseVisualStyleBackColor = true;
            // 
            // checkBox_move2
            // 
            this.checkBox_move2.AutoSize = true;
            this.checkBox_move2.Enabled = false;
            this.checkBox_move2.Location = new System.Drawing.Point(17, 104);
            this.checkBox_move2.Name = "checkBox_move2";
            this.checkBox_move2.Size = new System.Drawing.Size(87, 19);
            this.checkBox_move2.TabIndex = 7;
            this.checkBox_move2.Text = "Use Move 2";
            this.checkBox_move2.UseVisualStyleBackColor = true;
            // 
            // checkBox_move3
            // 
            this.checkBox_move3.AutoSize = true;
            this.checkBox_move3.Enabled = false;
            this.checkBox_move3.Location = new System.Drawing.Point(17, 129);
            this.checkBox_move3.Name = "checkBox_move3";
            this.checkBox_move3.Size = new System.Drawing.Size(87, 19);
            this.checkBox_move3.TabIndex = 8;
            this.checkBox_move3.Text = "Use Move 3";
            this.checkBox_move3.UseVisualStyleBackColor = true;
            // 
            // checkBox_move4
            // 
            this.checkBox_move4.AutoSize = true;
            this.checkBox_move4.Enabled = false;
            this.checkBox_move4.Location = new System.Drawing.Point(17, 154);
            this.checkBox_move4.Name = "checkBox_move4";
            this.checkBox_move4.Size = new System.Drawing.Size(87, 19);
            this.checkBox_move4.TabIndex = 9;
            this.checkBox_move4.Text = "Use Move 4";
            this.checkBox_move4.UseVisualStyleBackColor = true;
            // 
            // button_setBattlePixel
            // 
            this.button_setBattlePixel.Location = new System.Drawing.Point(123, 121);
            this.button_setBattlePixel.Name = "button_setBattlePixel";
            this.button_setBattlePixel.Size = new System.Drawing.Size(134, 23);
            this.button_setBattlePixel.TabIndex = 10;
            this.button_setBattlePixel.Text = "Set battle pixel (F5)";
            this.button_setBattlePixel.UseVisualStyleBackColor = true;
            this.button_setBattlePixel.Click += new System.EventHandler(this.button_setBattlePixel_Click);
            // 
            // button_startStop
            // 
            this.button_startStop.Location = new System.Drawing.Point(123, 150);
            this.button_startStop.Name = "button_startStop";
            this.button_startStop.Size = new System.Drawing.Size(134, 23);
            this.button_startStop.TabIndex = 11;
            this.button_startStop.Text = "Start/Stop bot (F6)";
            this.button_startStop.UseVisualStyleBackColor = true;
            this.button_startStop.Click += new System.EventHandler(this.button_startStop_Click);
            // 
            // textBox_notifications
            // 
            this.textBox_notifications.Location = new System.Drawing.Point(12, 179);
            this.textBox_notifications.Name = "textBox_notifications";
            this.textBox_notifications.Size = new System.Drawing.Size(401, 96);
            this.textBox_notifications.TabIndex = 12;
            this.textBox_notifications.Text = "";
            // 
            // pictureBox_battlePixel
            // 
            this.pictureBox_battlePixel.Location = new System.Drawing.Point(355, 104);
            this.pictureBox_battlePixel.Name = "pictureBox_battlePixel";
            this.pictureBox_battlePixel.Size = new System.Drawing.Size(25, 23);
            this.pictureBox_battlePixel.TabIndex = 13;
            this.pictureBox_battlePixel.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(333, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 15);
            this.label3.TabIndex = 14;
            this.label3.Text = "Battle Pixel";
            // 
            // label_status
            // 
            this.label_status.AutoSize = true;
            this.label_status.ForeColor = System.Drawing.Color.Red;
            this.label_status.Location = new System.Drawing.Point(17, 278);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(0, 15);
            this.label_status.TabIndex = 15;
            // 
            // label_r
            // 
            this.label_r.AutoSize = true;
            this.label_r.Location = new System.Drawing.Point(307, 144);
            this.label_r.Name = "label_r";
            this.label_r.Size = new System.Drawing.Size(11, 15);
            this.label_r.TabIndex = 16;
            this.label_r.Text = "r";
            // 
            // label_g
            // 
            this.label_g.AutoSize = true;
            this.label_g.Location = new System.Drawing.Point(351, 144);
            this.label_g.Name = "label_g";
            this.label_g.Size = new System.Drawing.Size(14, 15);
            this.label_g.TabIndex = 17;
            this.label_g.Text = "g";
            // 
            // label_b
            // 
            this.label_b.AutoSize = true;
            this.label_b.Location = new System.Drawing.Point(395, 144);
            this.label_b.Name = "label_b";
            this.label_b.Size = new System.Drawing.Size(14, 15);
            this.label_b.TabIndex = 18;
            this.label_b.Text = "b";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(110, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(165, 15);
            this.label4.TabIndex = 19;
            this.label4.Text = "* Put move with most PP on 1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 298);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label_b);
            this.Controls.Add(this.label_g);
            this.Controls.Add(this.label_r);
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox_battlePixel);
            this.Controls.Add(this.textBox_notifications);
            this.Controls.Add(this.button_startStop);
            this.Controls.Add(this.button_setBattlePixel);
            this.Controls.Add(this.checkBox_move4);
            this.Controls.Add(this.checkBox_move3);
            this.Controls.Add(this.checkBox_move2);
            this.Controls.Add(this.checkBox_move1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_movementTimeVariance);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_movementTime);
            this.Controls.Add(this.radioButton_upDown);
            this.Controls.Add(this.radioButton_leftRight);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_battlePixel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RadioButton radioButton_leftRight;
        private RadioButton radioButton_upDown;
        private TextBox textBox_movementTime;
        private Label label1;
        private TextBox textBox_movementTimeVariance;
        private Label label2;
        private CheckBox checkBox_move1;
        private CheckBox checkBox_move2;
        private CheckBox checkBox_move3;
        private CheckBox checkBox_move4;
        private Button button_setBattlePixel;
        private Button button_startStop;
        private RichTextBox textBox_notifications;
        private PictureBox pictureBox_battlePixel;
        private Label label3;
        private Label label_status;
        private Label label_r;
        private Label label_g;
        private Label label_b;
        private Label label4;
    }
}