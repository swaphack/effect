using System;

namespace Assets.Foundation.Net
{
    /// <summary>
    /// 接收buffer处理
    /// </summary>
    /// <param name="buff"></param>
    /// <returns></returns>
    public delegate int NetBufferDelegate(byte[] buff);
    /// <summary>
    /// 接收Message处理
    /// </summary>
    /// <param name="buff"></param>
    public delegate void NetMessageDelegate(byte[] buff);
    /// <summary>
    /// 接收客户端buffer处理
    /// </summary>
    /// <param name="buff"></param>
    public delegate int NetClientMessageDelegate(NetClient client, byte[] buff);
    /// <summary>
    /// 接收客户端Message处理
    /// </summary>
    /// <param name="buff"></param>
    public delegate void NetIDMessageDelegate(int msgId, byte[] buff);
    /// <summary>
    /// 接收客户端Message处理
    /// </summary>
    /// <param name="buff"></param>
    public delegate void NetClientIDMessageDelegate(int msgId, NetClient client, byte[] buff);
    /// <summary>
    /// 网络状态改变时通知
    /// </summary>
    /// <param name="client"></param>
    public delegate void NetStatusDelegate(NetSocket client);
    /// <summary>
    /// 客户端状态改变时通知
    /// </summary>
    /// <param name="client"></param>
    public delegate void NetClientDelegate(NetClient client);
}
