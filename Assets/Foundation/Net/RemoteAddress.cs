using System;
using System.Net;

namespace Game.Foundation.Net
{
    /// <summary>
    /// 远程地址
    /// </summary>
    public struct RemoteAddress
    {
        public string IP { get; set; }

        public int Port { get; set; }

        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(IP))
                {
                    return false;
                }
                IPHostEntry hostEntry = Dns.GetHostEntry(IP);
                if (hostEntry == null || hostEntry.AddressList == null || hostEntry.AddressList.Length == 0)
                {
                    return false;
                }

                return true;
            }
        }

        public RemoteAddress(string ip, int port)
        {
            IP = ip;
            Port = port;
        }
    }
}
