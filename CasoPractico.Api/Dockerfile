﻿FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim
WORKDIR /app

COPY ./publish/ /app
ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT docker
EXPOSE 5000

ENTRYPOINT ["dotnet", "/app/CasoPractico.Api.dll"]
