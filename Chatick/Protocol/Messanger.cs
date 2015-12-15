using Chatick.Protocol.NetHandlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chatick.Protocol
{
    public class Messanger
    {
        private BinaryReader input;
        private BinaryWriter output;
        private ClientHandler handler;
        private Thread readThread;
        public Users.User mainUser, oppositeUser;
        #region Events      
        public delegate void onChatMessage(String message);
        public event onChatMessage chatMessage;
        public delegate void onDisconnect();
        public event onDisconnect disconnectEvent;
        public event EventHandler<String> fileEvent;
        public void RaiseMessageEvent(String message)
        {
            if(chatMessage != null)
                chatMessage(message);
        }
        public void RaiseDisconnectEvent()
        {
            if (disconnectEvent != null)
                disconnectEvent();
        }

        public void RizeFileEvent(String filename)
        {
            if (fileEvent != null)
                fileEvent(null, filename);
        }

        #endregion
        public Messanger(Stream stream, string username)
        {
            input = new BinaryReader(stream);
            output = new BinaryWriter(stream);
            handler = new ClientHandler(this);
            mainUser = new Users.User(username);
            readThread = new Thread(() =>
            {
                try {
                    while (true)
                    {
                        GetPacket();
                    }
                }
                catch(IOException)
                {
                    this.Stop();

                }
                catch (ThreadAbortException)
                {

                }
            });
            readThread.IsBackground = true;
            readThread.Start();
            SendPacket(new Packets.PacketLogin(username));
        }
        public void Stop()
        {
            SendPacket(new Packets.PacketDisconnect());
            readThread.Abort();
            RaiseDisconnectEvent();
        }
        private void GetPacket()
        {
            byte ID = input.ReadByte();
            Packets.Packet packet = (Packets.Packet)Activator.CreateInstance(Packets.Packet.idMap[ID]);
            packet.read(input);
            packet.handle(handler);
        }
        public void SendPacket(Packets.Packet packet)
        {
            try {
                packet.write(output);
            }
            catch (IOException)
            {

            }
        }
    }
}
