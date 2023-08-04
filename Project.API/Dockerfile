#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Project.API/Project.API.csproj", "Project.API/"]
COPY ["Project.Infra.Components/Project.Infra.Components.csproj", "Project.Infra.Components/"]
COPY ["Project.Infra.Components.Interfaces/Project.Infra.Components.Interfaces.csproj", "Project.Infra.Components.Interfaces/"]
COPY ["Project.Redis/Project.Redis.csproj", "Project.Redis/"]
RUN dotnet restore "Project.API/Project.API.csproj"
COPY . .
WORKDIR "/src/Project.API"
RUN dotnet build "Project.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Project.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Project.API.dll"]