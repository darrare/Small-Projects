namespace SwaggerClassic
{
    partial class SwaggerClassic
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Button_RandomMovement = new System.Windows.Forms.Button();
            this.Button_QueuePasser = new System.Windows.Forms.Button();
            this.PictureBox_PixelBoxOriginal = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Label_MatchPercentage = new System.Windows.Forms.Label();
            this.TextBox_Log = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PictureBox_PixelBoxCurrent = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBx_processName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_PixelBoxOriginal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_PixelBoxCurrent)).BeginInit();
            this.SuspendLayout();
            // 
            // Button_RandomMovement
            // 
            this.Button_RandomMovement.Location = new System.Drawing.Point(166, 308);
            this.Button_RandomMovement.Name = "Button_RandomMovement";
            this.Button_RandomMovement.Size = new System.Drawing.Size(148, 36);
            this.Button_RandomMovement.TabIndex = 0;
            this.Button_RandomMovement.Text = "Start Random Movement";
            this.Button_RandomMovement.UseVisualStyleBackColor = true;
            this.Button_RandomMovement.Click += new System.EventHandler(this.Button_RandomMovement_Click);
            // 
            // Button_QueuePasser
            // 
            this.Button_QueuePasser.Location = new System.Drawing.Point(12, 308);
            this.Button_QueuePasser.Name = "Button_QueuePasser";
            this.Button_QueuePasser.Size = new System.Drawing.Size(148, 36);
            this.Button_QueuePasser.TabIndex = 1;
            this.Button_QueuePasser.Text = "Start Queue Passer";
            this.Button_QueuePasser.UseVisualStyleBackColor = true;
            this.Button_QueuePasser.Click += new System.EventHandler(this.Button_QueuePasser_Click);
            // 
            // PictureBox_PixelBoxOriginal
            // 
            this.PictureBox_PixelBoxOriginal.Location = new System.Drawing.Point(13, 141);
            this.PictureBox_PixelBoxOriginal.Name = "PictureBox_PixelBoxOriginal";
            this.PictureBox_PixelBoxOriginal.Size = new System.Drawing.Size(302, 55);
            this.PictureBox_PixelBoxOriginal.TabIndex = 2;
            this.PictureBox_PixelBoxOriginal.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Original";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // Label_MatchPercentage
            // 
            this.Label_MatchPercentage.AutoSize = true;
            this.Label_MatchPercentage.Location = new System.Drawing.Point(163, 125);
            this.Label_MatchPercentage.Name = "Label_MatchPercentage";
            this.Label_MatchPercentage.Size = new System.Drawing.Size(97, 13);
            this.Label_MatchPercentage.TabIndex = 5;
            this.Label_MatchPercentage.Text = "Match Percent: 0%";
            // 
            // TextBox_Log
            // 
            this.TextBox_Log.Location = new System.Drawing.Point(12, 29);
            this.TextBox_Log.Multiline = true;
            this.TextBox_Log.Name = "TextBox_Log";
            this.TextBox_Log.Size = new System.Drawing.Size(302, 93);
            this.TextBox_Log.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Information Log";
            // 
            // PictureBox_PixelBoxCurrent
            // 
            this.PictureBox_PixelBoxCurrent.Location = new System.Drawing.Point(12, 215);
            this.PictureBox_PixelBoxCurrent.Name = "PictureBox_PixelBoxCurrent";
            this.PictureBox_PixelBoxCurrent.Size = new System.Drawing.Size(302, 55);
            this.PictureBox_PixelBoxCurrent.TabIndex = 8;
            this.PictureBox_PixelBoxCurrent.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 199);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Current";
            // 
            // txtBx_processName
            // 
            this.txtBx_processName.Location = new System.Drawing.Point(94, 276);
            this.txtBx_processName.Name = "txtBx_processName";
            this.txtBx_processName.Size = new System.Drawing.Size(221, 20);
            this.txtBx_processName.TabIndex = 10;
            this.txtBx_processName.Text = "WowClassic";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 279);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Process Name";
            // 
            // SwaggerClassic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 356);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtBx_processName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PictureBox_PixelBoxCurrent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TextBox_Log);
            this.Controls.Add(this.Label_MatchPercentage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PictureBox_PixelBoxOriginal);
            this.Controls.Add(this.Button_QueuePasser);
            this.Controls.Add(this.Button_RandomMovement);
            this.Name = "SwaggerClassic";
            this.Text = "Swagger Classic";
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_PixelBoxOriginal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_PixelBoxCurrent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Button_RandomMovement;
        private System.Windows.Forms.Button Button_QueuePasser;
        private System.Windows.Forms.PictureBox PictureBox_PixelBoxOriginal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label Label_MatchPercentage;
        private System.Windows.Forms.TextBox TextBox_Log;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox PictureBox_PixelBoxCurrent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBx_processName;
        private System.Windows.Forms.Label label4;
    }
}

