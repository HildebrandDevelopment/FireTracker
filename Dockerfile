# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# ADDITIONAL TOOLS FOR DEBUGGING
ENV BUILDX_BUILDER=desktop-linux
ENV ACCEPT_EULA=Y

USER root
RUN apt-get -y update
RUN apt-get -y install curl

RUN curl https://packages.microsoft.com/keys/microsoft.asc | tee /etc/apt/trusted.gpg.d/microsoft.asc

RUN curl https://packages.microsoft.com/config/ubuntu/20.04/prod.list | tee /etc/apt/sources.list.d/mssql-release.list

RUN apt-get update
RUN apt-get install -y --no-install-recommends mssql-tools18 unixodbc-dev

# END ADDITIONAL TOOLS FOR DEBUGGING

USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HD.FireTracker.Web/HD.FireTracker.Web.csproj", "HD.FireTracker.Web/"]
COPY ["HD.FireTracker.Common.DTO/HD.FireTracker.Common.DTO.csproj", "HD.FireTracker.Common.DTO/"]
COPY ["HD.FireTracker.Common/HD.FireTracker.Common.csproj", "HD.FireTracker.Common/"]
COPY ["HD.FireTracker.Data.Common/HD.FireTracker.Data.Common.csproj", "HD.FireTracker.Data.Common/"]
COPY ["HD.FireTracker.Data.Service/HD.FireTracker.Data.Service.csproj", "HD.FireTracker.Data.Service/"]
COPY ["HD.FireTracker.DB.FireTrackerDB/HD.FireTracker.DB.FireTrackerDB.csproj", "HD.FireTracker.DB.FireTrackerDB/"]
RUN dotnet restore "./HD.FireTracker.Web/HD.FireTracker.Web.csproj"
COPY . .
WORKDIR "/src/HD.FireTracker.Web"
RUN dotnet build "./HD.FireTracker.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HD.FireTracker.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HD.FireTracker.Web.dll"]
