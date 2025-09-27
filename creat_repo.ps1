mkdir NetworkerChatDemo
cd NetworkerChatDemo

# 创建解决方案
dotnet new sln -n NetworkerChatDemo

# 创建项目
dotnet new classlib -n ChatSystem.Common
dotnet new console -n ChatSystem.Server
dotnet new console -n ChatSystem.Client

# 添加到解决方案
dotnet sln add ChatSystem.Common/ChatSystem.Common.csproj
dotnet sln add ChatSystem.Server/ChatSystem.Server.csproj
dotnet sln add ChatSystem.Client/ChatSystem.Client.csproj

# 创建 Networker 核心库（使用我们之前实现的代码）
dotnet new classlib -n Networker
dotnet new classlib -n Networker.Extensions.Json
dotnet sln add Networker/Networker.csproj
dotnet sln add Networker.Extensions.Json/Networker.Extensions.Json.csproj