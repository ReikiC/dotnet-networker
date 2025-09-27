using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networker.Common.Abstractions
{
    /// <summary>
    /// 数据包序列化器接口 - 把对象转换成字节，或把字节转换成对象
    /// </summary>
    public interface IPacketSerialiser
    {
        /// <summary>
        /// 把对象转换成字节数组（序列化）
        /// </summary>
        byte[] Serialise<T>(T packet);

        /// <summary>
        /// 把字节数组转换成对象（反序列化）
        /// </summary>
        T Deserialise<T>(byte[] packetBytes);
    }
}