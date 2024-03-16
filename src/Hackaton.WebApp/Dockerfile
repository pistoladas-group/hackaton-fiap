FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Hackaton.WebApp/Hackaton.WebApp.csproj", "src/Hackaton.WebApp/"]
RUN dotnet restore "src/Hackaton.WebApp/Hackaton.WebApp.csproj"
COPY . .
WORKDIR "/src/src/Hackaton.WebApp"
RUN dotnet build "Hackaton.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hackaton.WebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hackaton.WebApp.dll"]
