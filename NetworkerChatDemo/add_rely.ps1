# 配置 Networker 项目依赖
cd Networker
dotnet add package Microsoft.Extensions.DependencyInjection --version 2.2.0
dotnet add package Microsoft.Extensions.Logging --version 2.2.0
dotnet add package System.Memory --version 4.5.2
cd ..

# 配置 JSON 扩展依赖
cd Networker.Extensions.Json
dotnet add reference ../Networker/Networker.csproj
dotnet add package Newtonsoft.Json --version 12.0.3
cd ..

# 配置服务器依赖
cd ChatSystem.Server
dotnet add reference ../ChatSystem.Common/ChatSystem.Common.csproj
dotnet add reference ../Networker/Networker.csproj
dotnet add reference ../Networker.Extensions.Json/Networker.Extensions.Json.csproj
dotnet add package Microsoft.Extensions.Logging.Console --version 2.2.0
cd ..

# 配置客户端依赖
cd ChatSystem.Client
dotnet add reference ../ChatSystem.Common/ChatSystem.Common.csproj
dotnet add reference ../Networker/Networker.csproj
dotnet add reference ../Networker.Extensions.Json/Networker.Extensions.Json.csproj
dotnet add package Microsoft.Extensions.Logging.Console --version 2.2.0
cd ..