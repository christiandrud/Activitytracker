#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ActivityTracker.csproj", "."]
RUN dotnet restore "./ActivityTracker.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ActivityTracker.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "ActivityTracker.csproj" -c Debug -o /app/publish /p:UseAppHost=false
RUN dotnet dev-certs https --trust

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ActivityTracker.dll"]