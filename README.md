# Template Project

一个完整的 .NET 解决方案模板，包含以下组件：

- API 项目 (ASP.NET Core)
- Blazor WebAssembly 前端
- 核心库
- 消息/DTO 库
- 单元测试
- 集成测试

## 安装模板

### 从本地构建并安装

1. 克隆这个仓库 
2. 安装模板：
   ```
   dotnet new install .
   ```

## 使用模板

创建一个新项目：

```
dotnet new f-template -n YourProjectName
```

## 可用选项

- `-n|--name`：项目名称

## 项目结构

- **[ProjectName].Api**：WebAPI 项目
- **[ProjectName].Blazor.WASM**：Blazor WebAssembly 客户端
- **[ProjectName].Core**：核心业务逻辑
- **[ProjectName].Message**：DTO 和消息定义
- **[ProjectName].UnitTests**：单元测试项目
- **[ProjectName].IntegrationTests**：集成测试项目