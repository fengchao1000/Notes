#添加基础镜像
FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
#容器中系统的工作空间
WORKDIR /FCNotesWeb
#拷贝当前文件夹下的文件到容器中系统的工作空间
COPY . /FCNotesWeb
#设置Docker容器对外暴露的端口
EXPOSE 5000

#容器中使用 ["dotnet","系统启动的dll"] 来运行应用程序
#使用ENTRYPOINT ["dotnet","系统启动的dll"]
#或使用 CMD ["dotnet","系统启动的dll"]
ENTRYPOINT ["dotnet", "FC.Notes.Web.dll"]