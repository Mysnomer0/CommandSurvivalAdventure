namespace CommandSurvivalAdventure
{
    partial class ServerWindow
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
            this.HostServerButton = new System.Windows.Forms.Button();
            this.NameOfServerLabel = new System.Windows.Forms.Label();
            this.NameOfServerBox = new System.Windows.Forms.RichTextBox();
            this.HostServerOverLANCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // HostServerButton
            // 
            this.HostServerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.HostServerButton.Location = new System.Drawing.Point(12, 97);
            this.HostServerButton.Name = "HostServerButton";
            this.HostServerButton.Size = new System.Drawing.Size(323, 28);
            this.HostServerButton.TabIndex = 0;
            this.HostServerButton.Text = "Host Server!";
            this.HostServerButton.UseVisualStyleBackColor = true;
            this.HostServerButton.Click += new System.EventHandler(this.HostServerButton_Click);
            // 
            // NameOfServerLabel
            // 
            this.NameOfServerLabel.AutoSize = true;
            this.NameOfServerLabel.Location = new System.Drawing.Point(8, 14);
            this.NameOfServerLabel.Name = "NameOfServerLabel";
            this.NameOfServerLabel.Size = new System.Drawing.Size(103, 19);
            this.NameOfServerLabel.TabIndex = 1;
            this.NameOfServerLabel.Text = "Name of server";
            // 
            // NameOfServerBox
            // 
            this.NameOfServerBox.Location = new System.Drawing.Point(12, 36);
            this.NameOfServerBox.Name = "NameOfServerBox";
            this.NameOfServerBox.Size = new System.Drawing.Size(323, 25);
            this.NameOfServerBox.TabIndex = 2;
            this.NameOfServerBox.Text = "";
            // 
            // HostServerOverLANCheckBox
            // 
            this.HostServerOverLANCheckBox.AutoSize = true;
            this.HostServerOverLANCheckBox.Location = new System.Drawing.Point(13, 68);
            this.HostServerOverLANCheckBox.Name = "HostServerOverLANCheckBox";
            this.HostServerOverLANCheckBox.Size = new System.Drawing.Size(194, 23);
            this.HostServerOverLANCheckBox.TabIndex = 3;
            this.HostServerOverLANCheckBox.Text = "Host the server over LAN?";
            this.HostServerOverLANCheckBox.UseVisualStyleBackColor = true;
            // 
            // ServerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 317);
            this.Controls.Add(this.HostServerOverLANCheckBox);
            this.Controls.Add(this.NameOfServerBox);
            this.Controls.Add(this.NameOfServerLabel);
            this.Controls.Add(this.HostServerButton);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ServerWindow";
            this.Text = "Server ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button HostServerButton;
        private System.Windows.Forms.Label NameOfServerLabel;
        private System.Windows.Forms.RichTextBox NameOfServerBox;
        private System.Windows.Forms.CheckBox HostServerOverLANCheckBox;
    }
}