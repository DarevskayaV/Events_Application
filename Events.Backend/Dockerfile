FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Events.Back/Events.csproj", "Events.Back/"]
COPY ["Events.Application/Events.Application.csproj", "Events.Application/"]
COPY ["Events.Core/Events.Core.csproj", "Events.Core/"]
COPY ["Events.Data/Events.Data.csproj", "Events.Data/"]
RUN dotnet restore "Events.Back/Events.csproj"
COPY . .
WORKDIR "/src/Events.Back"
RUN dotnet build "Events.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Events.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Events.dll"]