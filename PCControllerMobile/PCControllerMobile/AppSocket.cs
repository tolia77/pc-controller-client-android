using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

namespace AppSocket
{
    public class SocketInfo
    {

        public IPEndPoint MoveMouseEndpoint { get; set; }
        public IPEndPoint GetScreenshotEndpoint { get; set; }
        public IPEndPoint OpenLinkEndpoint { get; set; }
        public IPEndPoint CMDExecuteEndpoint { get; set; }
        public IPEndPoint ShutdownPCEndpoint { get; set; }
        public IPEndPoint RestartPCEndpoint { get; set; }
        public IPAddress IpAddress { get; set; }
        public Socket AutoSocket
        {
            get { return new Socket(IpAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp); }
            set { }
        }

        public SocketInfo(IPAddress ipAddress)
        {
            IpAddress = ipAddress;
            MoveMouseEndpoint = new IPEndPoint(IpAddress, 50000);
            GetScreenshotEndpoint = new IPEndPoint(IpAddress, 50001);
            OpenLinkEndpoint = new IPEndPoint(IpAddress, 50002);
            CMDExecuteEndpoint = new IPEndPoint(IpAddress, 50003);
            ShutdownPCEndpoint = new IPEndPoint(IpAddress, 50004);
            RestartPCEndpoint = new IPEndPoint(IpAddress, 50005);
        }
        public byte[] ReceiveAll(Socket SocketSender)
        {
            var buffer = new List<byte>();

            while (true)
            {
                byte[] currByte = new byte[1];
                int byteCounter = SocketSender.Receive(currByte, currByte.Length, SocketFlags.None);
                if (byteCounter == 0)
                {
                    return buffer.ToArray();
                }
                else
                {
                    buffer.Add(currByte[0]);
                }
            }
        }
    }
}
