using System;
using System.Windows.Forms;

namespace CommandSurvivalAdventure
{
    public partial class ClientWindow : Form
    {
        CSACore application;

        public ClientWindow()
        {
            InitializeComponent();
            application = new CSACore(this);
            InputBox.KeyUp += OnKeyDownHandler;
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                application.input.OnReceiveInput(InputBox.Text);
                InputBox.Text = "";
            }
        }
    }
}
