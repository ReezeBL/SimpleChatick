using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chatick.Protocol.NetHandlers;

namespace Chatick.Protocol.Packets
{
    public class PacketDisconnect : Packet
    {
        public PacketDisconnect() { this.ID = 2; }

        public override void handle(IHandler handler)
        {
            handler.HandleDisconnectPacket(this);
        }
    }
}
