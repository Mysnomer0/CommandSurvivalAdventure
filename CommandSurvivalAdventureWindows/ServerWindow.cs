using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace CommandSurvivalAdventure
{
    partial class ServerWindow : Form
    {
        CSACore attachedApplication;

        public ServerWindow()
        {
            InitializeComponent();
        }
        public ServerWindow(CSACore attachedApplication)
        {
            InitializeComponent();
            this.attachedApplication = attachedApplication;
        }

        private void HostServerButton_Click(object sender, EventArgs e)
        {
            if(NameOfServerBox.Text != "")
            {
                // Start mosquitto
                if(HostServerOverLANCheckBox.Checked)
                {
                    using (Process mosquitto = new Process())
                    {
                        mosquitto.StartInfo.UseShellExecute = false;
                        mosquitto.StartInfo.FileName = ".\\mosquitto\\mosquitto.exe";
                        mosquitto.StartInfo.CreateNoWindow = false;
                        mosquitto.Start();
                    }
                    // Run the server in the current thread
                    attachedApplication.server.Start("localhost", 1883, NameOfServerBox.Text);
                }
                else
                {
                    // Run the server in the current thread
                    attachedApplication.server.Start("test.mosquitto.org", 1883, NameOfServerBox.Text);
                }
            }
        }
    }
}
