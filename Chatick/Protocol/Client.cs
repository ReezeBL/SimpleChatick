using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Chatick.Protocol
{
    public class Client
    {
        ConnectionManager manager;
        TcpClient client;

        public Client(ConnectionManager manager)
        {
            this.manager = manager;
        }

        public void Connect(String IP)
        {
            String[] split = IP.Split(':');
            if(split.Length < 2)
            {
                manager.Message("Invalid IP adress");
            }
            client = new TcpClient(split[0], Convert.ToInt32(split[1]));
            manager.StreamConnect(client.GetStream());
        }
    }
}
