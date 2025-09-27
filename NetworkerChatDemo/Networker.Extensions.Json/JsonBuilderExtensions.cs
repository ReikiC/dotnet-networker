using Microsoft.Extensions.DependencyInjection;
using Networker.Common.Abstractions;

namespace Networker.Extensions.Json
{
    /// <summary>
    /// JSON 扩展方法 - 让我们可以用 .UseJson() 来配置
    /// </summary>
    public static class JsonBuilderExtensions
    {
        /// <summary>
        /// 为服务器添加 JSON 序列化器
        /// </summary>
        public static IServiceCollection UseJson(this IServiceCollection services)
        {
            services.AddSingleton<IPacketSerialiser, JsonSerialiser>();
            return services;
        }
    }
}