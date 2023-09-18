FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["QueueingTheory/QueueingTheory.csproj", "QueueingTheory/"]
RUN dotnet restore "QueueingTheory/QueueingTheory.csproj"
COPY . .
WORKDIR "/src/QueueingTheory"
RUN dotnet build "QueueingTheory.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QueueingTheory.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QueueingTheory.dll"]
