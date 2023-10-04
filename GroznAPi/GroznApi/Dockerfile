FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GroznApi/GroznApi.csproj", "GroznApi/"]
RUN dotnet restore "GroznApi/GroznApi.csproj"
COPY . .
WORKDIR "/src/GroznApi"
RUN dotnet build "GroznApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GroznApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GroznApi.dll"]
