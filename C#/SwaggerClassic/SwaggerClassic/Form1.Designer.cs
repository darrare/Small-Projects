namespace SwaggerClassic
{
    partial class Form1
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
            this.Button_Toggle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Button_Toggle
            // 
            this.Button_Toggle.Location = new System.Drawing.Point(12, 12);
            this.Button_Toggle.Name = "Button_Toggle";
            this.Button_Toggle.Size = new System.Drawing.Size(148, 74);
            this.Button_Toggle.TabIndex = 0;
            this.Button_Toggle.Text = "Start";
            this.Button_Toggle.UseVisualStyleBackColor = true;
            this.Button_Toggle.Click += new System.EventHandler(this.Button_Toggle_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(176, 103);
            this.Controls.Add(this.Button_Toggle);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Button_Toggle;
    }
}

