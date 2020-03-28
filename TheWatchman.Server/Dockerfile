#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM node:10-alpine as build-node
WORKDIR /ClientApp
COPY ["TheWatchman.Server/ClientApp/package.json", "."]
COPY ["TheWatchman.Server/ClientApp/package-lock.json", "."]
RUN npm install
COPY TheWatchman.Server/ClientApp/ . 
RUN npm run build

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
ENV BuildingDocker true
WORKDIR /src
COPY ["TheWatchman.sln", "."]
COPY ["TheWatchman.Core/TheWatchman.Core.csproj", "TheWatchman.Core/"]
COPY ["TheWatchman.OtpGenerator/TheWatchman.OtpGenerator.csproj", "TheWatchman.OtpGenerator/"]
COPY ["TheWatchman.Server/TheWatchman.Server.csproj", "TheWatchman.Server/"]
COPY ["TheWatchman.Monitor/TheWatchman.Monitor.csproj", "TheWatchman.Monitor/"]

RUN dotnet restore "TheWatchman.sln"
COPY . .
RUN dotnet build "TheWatchman.sln" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TheWatchman.Server/TheWatchman.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build-node /ClientApp/build ./publish/ClientApp/build
ENTRYPOINT ["dotnet", "TheWatchman.Server.dll"]
