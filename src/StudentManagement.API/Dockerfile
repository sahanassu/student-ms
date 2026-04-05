FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/StudentManagement.API/StudentManagement.API.csproj", "src/StudentManagement.API/"]
COPY ["src/StudentManagement.Core/StudentManagement.Core.csproj", "src/StudentManagement.Core/"]
COPY ["src/StudentManagement.Infrastructure/StudentManagement.Infrastructure.csproj", "src/StudentManagement.Infrastructure/"]
RUN dotnet restore "src/StudentManagement.API/StudentManagement.API.csproj"
COPY . .
WORKDIR "/src/src/StudentManagement.API"
RUN dotnet build "StudentManagement.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StudentManagement.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StudentManagement.API.dll"]
