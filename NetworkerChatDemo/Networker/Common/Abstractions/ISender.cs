using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Networker.Common.Abstractions
{
    /// <summary>
    /// 发送器接口 - 用于发送数据包
    /// </summary>
    public interface ISender
    {
        /// <summary>
        /// 远程端点（客户端的IP和端口）
        /// </summary>
        IPEndPoint EndPoint { get; }

        /// <summary>
        /// 发送数据包
        /// </summary> 
        void Send<T>(T packet);
    }
}
