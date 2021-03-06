﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chatick.Protocol;
using System.IO;

namespace Chatick
{
    public partial class ChatForm : Form
    {
        private Messanger msgr;
        private delegate void printStr(String msg);
        public ChatForm(ConnectionManager manager)
        {
            InitializeComponent();
            this.msgr = manager.createNewMessanger();
            this.Text += " " + msgr.mainUser.username;
            msgr.chatMessage += Msgr_chatMessage;
            msgr.disconnectEvent += Msgr_disconnectEvent;
            msgr.fileEvent += Msgr_fileEvent;
        }

        private void Msgr_fileEvent(object sender, string e)
        {
            chatTextBox.AppendText("Полуаю файл " + e + Environment.NewLine);
        }

        private void Msgr_disconnectEvent()
        {
            MessageBox.Show("Companion has disconnected!","Alert",MessageBoxButtons.OK);
            this.Close();
        }

        private void Msgr_chatMessage(string message)
        {
            if (this.InvokeRequired)
                this.Invoke(new printStr(Msgr_chatMessage),message);
            else
                chatTextBox.AppendText(message + Environment.NewLine);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            msgr.SendPacket(new Protocol.Packets.PacketMessage(messageTextBox.Text));
            chatTextBox.AppendText(msgr.mainUser.username + ": " + messageTextBox.Text + Environment.NewLine);
            messageTextBox.Text = "";
        }

        private void messageTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void ChatForm_Load(object sender, EventArgs e)  
        {

        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            msgr.SendPacket(new Protocol.Packets.PacketDisconnect());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string filename = openFileDialog1.FileName;
            if (filename != null)
            {
                msgr.SendPacket(new Protocol.Packets.PacketFileExchnage(1, Encoding.UTF8.GetBytes(openFileDialog1.SafeFileName)));
                msgr.SendPacket(new Protocol.Packets.PacketFileExchnage(2, File.ReadAllBytes(filename)));
            }
        }
    }
}
