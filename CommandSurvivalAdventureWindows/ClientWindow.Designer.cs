namespace CommandSurvivalAdventure
{
    partial class ClientWindow
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
            this.OutputBox = new System.Windows.Forms.RichTextBox();
            this.InputBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // OutputBox
            // 
            this.OutputBox.BackColor = System.Drawing.SystemColors.Control;
            this.OutputBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.OutputBox.CausesValidation = false;
            this.OutputBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.OutputBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.OutputBox.Enabled = false;
            this.OutputBox.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.OutputBox.ForeColor = System.Drawing.Color.Black;
            this.OutputBox.ImeMode = System.Windows.Forms.ImeMode.On;
            this.OutputBox.Location = new System.Drawing.Point(0, 0);
            this.OutputBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.OutputBox.Name = "OutputBox";
            this.OutputBox.ReadOnly = true;
            this.OutputBox.Size = new System.Drawing.Size(1380, 406);
            this.OutputBox.TabIndex = 1;
            this.OutputBox.TabStop = false;
            this.OutputBox.Text = "";
            // 
            // InputBox
            // 
            this.InputBox.BackColor = System.Drawing.Color.Gainsboro;
            this.InputBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.InputBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputBox.ForeColor = System.Drawing.Color.Black;
            this.InputBox.Location = new System.Drawing.Point(0, 737);
            this.InputBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.InputBox.Multiline = false;
            this.InputBox.Name = "InputBox";
            this.InputBox.Size = new System.Drawing.Size(1380, 22);
            this.InputBox.TabIndex = 2;
            this.InputBox.Text = "";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1380, 759);
            this.Controls.Add(this.InputBox);
            this.Controls.Add(this.OutputBox);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MainWindow";
            this.Text = "Command Survival Adventure";
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.RichTextBox OutputBox;
        private System.Windows.Forms.RichTextBox InputBox;
    }
}

