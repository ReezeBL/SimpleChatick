using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chatick.Protocol;

namespace Chatick
{
    public partial class LoginForm : Form
    {
        ConnectionManager manager;
        private delegate void invokation();
        public LoginForm()
        {          
            manager = new ConnectionManager();
            manager.connectMessage += Manager_connectMessage;
            InitializeComponent();
        }

        private void Manager_connectMessage()
        {
            if (InvokeRequired)
                Invoke(new invokation(Manager_connectMessage));
            else
            {
                ChatForm form = new ChatForm(manager);
                this.Hide();
                form.ShowDialog();
                this.Close();
            }
        }

        private void logInButton_Click(object sender, EventArgs e)
        {
            manager.Connect(ipTextBox.Text);
            if (manager.Connected)
            {
                ChatForm form = new ChatForm(manager);
                this.Hide();
                form.ShowDialog();
                this.Close();
            }
        }

        private void nicknameTextBox_TextChanged(object sender, EventArgs e)
        {
            manager.Name = nicknameTextBox.Text;
        }
    }
}
