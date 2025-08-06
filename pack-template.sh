#!/bin/bash

# 清理临时文件
find . -name "bin" -type d -exec rm -rf {} +
find . -name "obj" -type d -exec rm -rf {} +

# 直接从当前目录安装模板
dotnet new install . --force

echo "模板已直接安装成功。"
echo "可以使用以下命令创建新项目："
echo "dotnet new f-template -n 您的项目名称"