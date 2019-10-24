using System;

namespace Assets.Foundation.Net
{
    /// <summary>
    /// 远程地址
    /// </summary>
    public struct RemoteAddress
    {
        /// <summary>
        /// 地址
        /// </summary>
        private string _ip;
        /// <summary>
        /// 端口
        /// </summary>
        private int _port;

        public string IP
        {
            get
            {
                return _ip;
            }
            set
            {
                _ip = value;
            }
        }

        public int Port
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
            }
        }

        public RemoteAddress(string ip, int port)
        {
            _ip = ip;
            _port = port;
        }
    }
}
