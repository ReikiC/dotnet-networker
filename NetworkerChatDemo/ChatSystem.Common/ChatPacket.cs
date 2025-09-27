using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem.Common
{
    /// <summary>
    /// 聊天消息数据包 - 这就是我们要在网络上传输的数据
    /// </summary>
    [Serializable]
    public class ChatPacket
    {
        /// <summary>
        /// 发送消息的用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
