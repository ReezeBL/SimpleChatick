using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatick.Protocol.NetHandlers
{
    public abstract class IHandler
    {
        public abstract void HandleLoginPacket(Packets.PacketLogin packet);
        public abstract void HandleMessagePacket(Packets.PacketMessage packet);
        public abstract void HandleDisconnectPacket(Packets.PacketDisconnect packet);
        public abstract void HandleFileExchangePacket(Packets.PacketFileExchnage packet);
    }
}
