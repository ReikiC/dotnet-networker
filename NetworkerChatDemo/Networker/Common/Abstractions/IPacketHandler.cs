using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networker.Common.Abstractions
{
    /// <summary>
    /// 数据包处理器接口
    /// </summary>
    public interface IPacketHandler
    {
        /// <summary>
        /// 处理数据包
        /// </summary>
        Task Handle(IPacketContext packetContext);
    }
}