using System;

namespace Assets.Foundation.Net
{
    public delegate int NetBufferDelegate(byte[] buff);

    public delegate void NetMessageDelegate(byte[] buff);
}
