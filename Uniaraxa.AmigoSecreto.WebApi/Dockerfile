#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0.2-alpine3.18-arm64v8 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Uniaraxa.AmigoSecreto.WebApi/Uniaraxa.AmigoSecreto.WebApi.csproj", "Uniaraxa.AmigoSecreto.WebApi/"]
COPY ["Uniaraxa.AmigoSecreto.Infrastructure/Uniaraxa.AmigoSecreto.Infrastructure.csproj", "Uniaraxa.AmigoSecreto.Infrastructure/"]
COPY ["Uniaraxa.AmigoSecreto.Application/Uniaraxa.AmigoSecreto.Application.csproj", "Uniaraxa.AmigoSecreto.Application/"]
COPY ["Uniaraxa.AmigoSecreto.Core/Uniaraxa.AmigoSecreto.Core.csproj", "Uniaraxa.AmigoSecreto.Core/"]
RUN dotnet restore "./Uniaraxa.AmigoSecreto.WebApi/Uniaraxa.AmigoSecreto.WebApi.csproj"
COPY . .
WORKDIR "/src/Uniaraxa.AmigoSecreto.WebApi"
RUN dotnet build "./Uniaraxa.AmigoSecreto.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Uniaraxa.AmigoSecreto.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Uniaraxa.AmigoSecreto.WebApi.dll"]