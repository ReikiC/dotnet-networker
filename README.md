# 项目设计思路和结构全面分析

## 🏢 项目物理结构

```
NetworkerChatDemo/
├── NetworkerChatDemo.sln
├── Networker/
│   ├── Networker.csproj
│   └── Common/
│       ├── Abstractions/
│       │   ├── IPacketSerialiser.cs
│       │   ├── ISender.cs
│       │   ├── IPacketContext.cs
│       │   └── IPacketHandler.cs
│       ├── PacketContext.cs
│       └── PacketHandlerBase.cs
├── Networker.Extensions.Json/
│   ├── Networker.Extensions.Json.csproj
│   ├── JsonSerialiser.cs
│   └── JsonBuilderExtensions.cs
├── ChatSystem.Common/
│   ├── ChatSystem.Common.csproj
│   └── ChatPacket.cs
├── ChatSystem.Server/
│   ├── ChatSystem.Server.csproj
│   └── Program.cs
└── ChatSystem.Client/
    ├── ChatSystem.Client.csproj
    └── Program.cs
```
