FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY ["MyProducer/MyProducer.csproj", "MyProducer/"]
RUN dotnet restore "MyProducer/MyProducer.csproj"
COPY . .
WORKDIR "/MyProducer"
RUN dotnet build "MyProducer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyProducer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyProducer.dll"]