using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Windows.Forms;

namespace Chatick.Protocol
{
    public class ConnectionManager
    {
        private bool connected;
        private Stream connection_stream;
        private Client client;
        private Server server;
        private string nickname;

        #region Events
        public delegate void onConnectMessage();
        public event onConnectMessage connectMessage;
        public void RaiseConnectMessage()
        {
            if (connectMessage != null)
                connectMessage();
        }
        #endregion
        public string Name
        {
            get{return nickname; }
            set {nickname = value; }           
        }
        public ConnectionManager()
        {
            nickname = "";
            client = new Client(this);
            server = new Server(this);
            server.Run();   
        }
        
        public Messanger createNewMessanger()
        {
           return new Messanger(connection_stream, Name);
        }
        #region Connections
        public void StreamConnect(Stream s) {
            this.connected = true;
            connection_stream = s;
        }
        public void Connect(String IP)
        {
            try {
                client.Connect(IP);
                connected = true;
            }
            catch (SocketException)
            {
                Message("Unable to connect to server!");
                connected = false;
            }
            catch (IOException)
            {
                Message("User disconnected");
                connected = false;
            }
            catch (Exception)
            {
                Message("Unknown error");
                connected = false;
            }
        }
        public void RunServer()
        {
            if (!server.Running)
                server.Run();
        }
        public bool Connected
        {
            get { return connected; }
        }
        #endregion
        #region Application
        public void Message(String message) {
            MessageBox.Show(message, "Attention");
        }
        #endregion
    }
}
