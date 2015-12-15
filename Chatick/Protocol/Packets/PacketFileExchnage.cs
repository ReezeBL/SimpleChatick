using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chatick.Protocol.NetHandlers;

namespace Chatick.Protocol.Packets
{
    public class PacketFileExchnage : Packet
    {
        public int mode;
        public byte[] data;
        public PacketFileExchnage()
        {
            ID = 4;
        }

        public PacketFileExchnage(int mode, byte[] data)
        {
            ID = 4;
            this.mode = mode;
            this.data = data;
        }

        public override void write(BinaryWriter DataOutput)
        {
            base.write(DataOutput);
            DataOutput.Write(mode);
            DataOutput.Write(data.Length);
            DataOutput.Write(data);
        }

        public override void read(BinaryReader DataInput)
        {
            base.read(DataInput);
            mode = DataInput.ReadInt32();
            int l = DataInput.ReadInt32();
            data = DataInput.ReadBytes(l); ;
        }

        public override void handle(IHandler handler)
        {
            handler.HandleFileExchangePacket(this);
        }
    }
}
