using System;
using Networker.Common.Abstractions;

namespace Networker.Common
{
    /// <summary>
    /// 数据包上下文实现
    /// </summary>
    public class PacketContext : IPacketContext
    {
        public byte[] PacketBytes { get; set; }
        public ISender Sender { get; set; }
        public IPacketSerialiser Serialiser { get; set; }

        /// <summary>
        /// 获取强类型数据包对象
        /// </summary>
        public T GetPacket<T>() where T : class
        {
            return Serialiser.Deserialise<T>(PacketBytes);
        }
    }
}