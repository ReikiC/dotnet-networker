using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networker.Common.Abstractions
{
    /// <summary>
    /// 数据包上下文 - 包含处理数据包需要的所有信息
    /// </summary>
    public interface IPacketContext
    {
        /// <summary>
        /// 原始数据包字节
        /// </summary>
        byte[] PacketBytes { get; set; }

        /// <summary>
        /// 发送者（可以用来回复消息）
        /// </summary>
        ISender Sender { get; set; }

        /// <summary>
        /// 序列化器
        /// </summary>
        IPacketSerialiser Serialiser { get; set; }

        /// <summary>
        /// 获取强类型的数据包对象
        /// </summary>
        T GetPacket<T>() where T : class;
    }
}