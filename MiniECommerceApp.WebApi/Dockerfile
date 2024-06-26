# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

EXPOSE 8080
EXPOSE 8081

ENV Redis=${Redis}
ENV rabbitmq=${rabbitmq}
ENV ConnectionStrings=${ConnectionStrings}
ENV STRIPE_SECRET_KEY=${STRIPE_SECRET_KEY}
ENV STRIPE_PUBLISHABLE_KEY=${STRIPE_PUBLISHABLE_KEY}
ENV STRIPE_WEBHOOK_SECRET=${STRIPE_WEBHOOK_SECRET}


COPY --chown=app ["MiniECommerceApp.WebApi/Media", "/app/Media"]

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MiniECommerceApp.WebApi/MiniECommerceApp.WebApi.csproj", "MiniECommerceApp.WebApi/"]
COPY ["MiniECommerceApp.Application/MiniECommerceApp.Application.csproj", "MiniECommerceApp.Application/"]
COPY ["MiniECommerceApp.Data/MiniECommerceApp.Data.csproj", "MiniECommerceApp.Data/"]
COPY ["MiniECommerceApp.Entity/MiniECommerceApp.Entity.csproj", "MiniECommerceApp.Entity/"]
COPY ["MiniECommerceApp.Core/MiniECommerceApp.Core.csproj", "MiniECommerceApp.Core/"]
RUN dotnet restore "./MiniECommerceApp.WebApi/MiniECommerceApp.WebApi.csproj"
COPY . .
WORKDIR "/src/MiniECommerceApp.WebApi"
RUN dotnet build "./MiniECommerceApp.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MiniECommerceApp.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MiniECommerceApp.WebApi.dll"]
