using Networker.Common.Abstractions;
using System.Threading.Tasks;

namespace Networker.Common
{
    /// <summary>
    /// 数据包处理器基类 - 这是一个模板，让处理不同类型数据包变得简单
    /// </summary>
    public abstract class PacketHandlerBase<T> : IPacketHandler where T : class
    {
        /// <summary>
        /// 处理数据包（框架调用）
        /// </summary>
        public async Task Handle(IPacketContext context)
        {
            // 自动把字节转换成强类型对象
            var packet = context.Serialiser.Deserialise<T>(context.PacketBytes);

            // 调用子类的具体处理逻辑
            await Process(packet, context);
        }

        /// <summary>
        /// 子类必须实现这个方法来处理具体的数据包
        /// </summary>
        public abstract Task Process(T packet, IPacketContext context);
    }
}