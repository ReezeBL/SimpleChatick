using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace Chatick.Protocol
{
    public class Server
    {
        ConnectionManager manager;   
        Thread serverThread;
        TcpListener serverListener;
        bool isRunning;
        public bool Running
        {
            get { return isRunning; }
        }
        public Server(ConnectionManager manager)
        {
            this.manager = manager;
        }
        public void Run()
        {
            serverListener = new TcpListener(IPAddress.Any, getPort());
            serverListener.Start();        
            serverThread = new Thread(() =>
             {
                 TcpClient client = serverListener.AcceptTcpClient();               
                 manager.StreamConnect(client.GetStream());
                 manager.RaiseConnectMessage();              
                 Stop();
             });
            serverThread.IsBackground = true;
            serverThread.Start();
            isRunning = true;
        }

        public void Stop()
        {
            serverThread.Abort();
            serverListener.Stop();           
            isRunning = false;
        }

        private int getPort()
        {
            StreamReader sr = new StreamReader("Settings.txt");
            int res = Convert.ToInt32(sr.ReadLine());
            sr.Close();
            return res;
        }
    }
}
