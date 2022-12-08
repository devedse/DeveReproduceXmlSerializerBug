#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim-amd64 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "DeveReproduceXmlSerializerBug.ConsoleApp/DeveReproduceXmlSerializerBug.ConsoleApp.csproj"
WORKDIR "/src/DeveReproduceXmlSerializerBug.ConsoleApp"
RUN dotnet build "DeveReproduceXmlSerializerBug.ConsoleApp.csproj" -c Release -o /app/build
RUN dotnet test

FROM build AS publish
ARG BUILD_VERSION
ARG VER=${BUILD_VERSION:-1.0.0}
RUN dotnet publish "DeveReproduceXmlSerializerBug.ConsoleApp.csproj" -c Release -o /app/publish /p:Version=$VER /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeveReproduceXmlSerializerBug.ConsoleApp.dll"]