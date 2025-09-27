// 文件位置: ChatSystem.Client/Program.cs
using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using ChatSystem.Common;
using Networker.Common.Abstractions;
using Networker.Extensions.Json;

namespace ChatSystem.Client
{
    class Program
    {
        private static TcpClient tcpClient;
        private static NetworkStream stream;
        private static IPacketSerialiser serialiser = new JsonSerialiser();
        private static string username;

        static void Main(string[] args)
        {
            Console.WriteLine("=== 简单聊天客户端 ===");

            // 获取用户名
            Console.Write("请输入您的用户名: ");
            username = Console.ReadLine();

            // 连接到服务器
            if (ConnectToServer())
            {
                Console.WriteLine("连接成功！");
                Console.WriteLine("现在可以开始聊天了，输入 'quit' 退出");

                // 启动接收消息的线程
                var receiveThread = new Thread(ReceiveMessages);
                receiveThread.Start();

                // 主线程处理用户输入
                HandleUserInput();

                // 等待接收线程结束
                receiveThread.Join();
            }
            else
            {
                Console.WriteLine("连接失败！");
            }

            Console.WriteLine("程序结束，按任意键退出...");
            Console.ReadKey();
        }

        /// <summary>
        /// 连接到服务器
        /// </summary>
        static bool ConnectToServer()
        {
            try
            {
                tcpClient = new TcpClient();
                tcpClient.Connect("127.0.0.1", 8080);
                stream = tcpClient.GetStream();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"连接服务器失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 处理用户输入
        /// </summary>
        static void HandleUserInput()
        {
            string input;
            while ((input = Console.ReadLine()) != "quit")
            {
                if (!string.IsNullOrWhiteSpace(input))
                {
                    SendMessage(input);
                }
            }

            // 用户输入 quit，关闭连接
            tcpClient?.Close();
        }

        /// <summary>
        /// 发送消息到服务器
        /// </summary>
        static void SendMessage(string message)
        {
            try
            {
                var chatPacket = new ChatPacket
                {
                    Username = username,
                    Message = message,
                    Timestamp = DateTime.Now
                };

                // 序列化并发送
                var data = serialiser.Serialise(chatPacket);
                stream.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发送消息失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 接收服务器消息（在单独线程中运行）
        /// </summary>
        static void ReceiveMessages()
        {
            var buffer = new byte[1024];

            try
            {
                while (tcpClient.Connected)
                {
                    // 读取服务器发送的数据
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                    {
                        // 服务器断开连接
                        Console.WriteLine("与服务器的连接已断开");
                        break;
                    }

                    // 只取实际读取的字节
                    var actualData = new byte[bytesRead];
                    Array.Copy(buffer, actualData, bytesRead);

                    // 反序列化消息
                    var chatPacket = serialiser.Deserialise<ChatPacket>(actualData);

                    // 显示消息（不显示自己发送的消息）
                    if (chatPacket.Username != username)
                    {
                        Console.WriteLine($"[{chatPacket.Timestamp:HH:mm:ss}] {chatPacket.Username}: {chatPacket.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"接收消息时出错: {ex.Message}");
            }
        }
    }
}