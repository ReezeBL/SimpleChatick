using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chatick.Protocol.Packets;
using System.IO;

namespace Chatick.Protocol.NetHandlers
{
    public class ClientHandler : IHandler
    {
        private String filename;
        private Messanger msgr;
        public ClientHandler(Messanger msgr)
        {
            this.msgr = msgr;
        }

        public override void HandleDisconnectPacket(PacketDisconnect packet)
        {
            msgr.RaiseDisconnectEvent();
        }

        public override void HandleMessagePacket(PacketMessage packet)
        {
            msgr.RaiseMessageEvent(msgr.oppositeUser.username + ":" + packet.Message);
        }

        public override void HandleLoginPacket(PacketLogin packet)
        {
            msgr.oppositeUser = new Users.User(packet.Name);
        }

        public override void HandleFileExchangePacket(PacketFileExchnage packet)
        {
            if(packet.mode == 1)
            {
                filename = Encoding.UTF8.GetString(packet.data);
            }
            else if(packet.mode == 2)
            {
                File.WriteAllBytes(filename, packet.data);
            }
        }
    }
}
