FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY ["MyConsumer/MyConsumer.csproj", "MyConsumer/"]
RUN dotnet restore "MyConsumer/MyConsumer.csproj"
COPY . .
WORKDIR "/MyConsumer"
RUN dotnet build "MyConsumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyConsumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyConsumer.dll"]