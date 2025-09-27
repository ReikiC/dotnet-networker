// 文件位置: ChatSystem.Server/Program.cs
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using ChatSystem.Common;
using Networker.Common;
using Networker.Common.Abstractions;
using Networker.Extensions.Json;

namespace ChatSystem.Server
{
    class Program
    {
        // 保存所有连接的客户端
        private static List<ClientConnection> clients = new List<ClientConnection>();
        private static IPacketSerialiser serialiser = new JsonSerialiser();

        static void Main(string[] args)
        {
            Console.WriteLine("=== 简单聊天服务器 ===");
            Console.WriteLine("正在启动服务器...");

            // 创建 TCP 监听器
            var listener = new TcpListener(IPAddress.Any, 8080);
            listener.Start();

            Console.WriteLine("服务器已启动在端口 8080");
            Console.WriteLine("等待客户端连接...");

            // 持续接受客户端连接
            while (true)
            {
                try
                {
                    // 等待客户端连接
                    var tcpClient = listener.AcceptTcpClient();
                    Console.WriteLine($"新客户端连接: {tcpClient.Client.RemoteEndPoint}");

                    // 为每个客户端创建一个处理线程
                    var clientConnection = new ClientConnection(tcpClient, serialiser);
                    clients.Add(clientConnection);

                    // 启动处理线程
                    var thread = new Thread(() => HandleClient(clientConnection));
                    thread.Start();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"接受连接时出错: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 处理单个客户端的消息
        /// </summary>
        static void HandleClient(ClientConnection client)
        {
            try
            {
                var buffer = new byte[1024];
                var stream = client.TcpClient.GetStream();

                while (client.TcpClient.Connected)
                {
                    // 读取客户端发送的数据
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                    {
                        // 客户端断开连接
                        break;
                    }

                    // 只取实际读取的字节
                    var actualData = new byte[bytesRead];
                    Array.Copy(buffer, actualData, bytesRead);

                    // 反序列化消息
                    var chatPacket = serialiser.Deserialise<ChatPacket>(actualData);

                    Console.WriteLine($"收到消息: {chatPacket.Username}: {chatPacket.Message}");

                    // 广播给所有客户端
                    BroadcastMessage(chatPacket);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"处理客户端时出错: {ex.Message}");
            }
            finally
            {
                // 客户端断开，从列表中移除
                clients.Remove(client);
                client.TcpClient.Close();
                Console.WriteLine("客户端断开连接");
            }
        }

        /// <summary>
        /// 广播消息给所有客户端
        /// </summary>
        static void BroadcastMessage(ChatPacket packet)
        {
            var data = serialiser.Serialise(packet);

            // 发送给所有连接的客户端
            foreach (var client in clients.ToArray()) // 用 ToArray() 避免在遍历时修改集合
            {
                try
                {
                    if (client.TcpClient.Connected)
                    {
                        var stream = client.TcpClient.GetStream();
                        stream.Write(data, 0, data.Length);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"发送消息给客户端时出错: {ex.Message}");
                }
            }
        }
    }

    /// <summary>
    /// 客户端连接信息
    /// </summary>
    public class ClientConnection
    {
        public TcpClient TcpClient { get; }
        public IPacketSerialiser Serialiser { get; }

        public ClientConnection(TcpClient tcpClient, IPacketSerialiser serialiser)
        {
            TcpClient = tcpClient;
            Serialiser = serialiser;
        }
    }
}