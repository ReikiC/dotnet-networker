using Networker.Common.Abstractions;
using Newtonsoft.Json;
using System.Text;

namespace Networker.Extensions.Json
{
    /// <summary>
    /// JSON 序列化器 - 把对象转换成 JSON 字符串，再转换成字节
    /// </summary>
    public class JsonSerialiser : IPacketSerialiser
    {
        /// <summary>
        /// 把对象转换成字节数组
        /// </summary>
        public byte[] Serialise<T>(T packet)
        {
            // 1. 把对象转换成 JSON 字符串
            var json = JsonConvert.SerializeObject(packet);

            // 2. 把 JSON 字符串转换成字节数组
            var bytes = Encoding.UTF8.GetBytes(json);

            return bytes;
        }

        /// <summary>
        /// 把字节数组转换成对象
        /// </summary>
        public T Deserialise<T>(byte[] packetBytes)
        {
            // 1. 把字节数组转换成 JSON 字符串
            var json = Encoding.UTF8.GetString(packetBytes);

            // 2. 把 JSON 字符串转换成对象
            var obj = JsonConvert.DeserializeObject<T>(json);

            return obj;
        }
    }
}