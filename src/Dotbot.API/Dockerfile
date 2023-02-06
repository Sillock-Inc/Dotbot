FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Dotbot.API/Dotbot.API.csproj", "Dotbot.API/"]
RUN dotnet restore "Dotbot.API/Dotbot.API.csproj"
COPY . .
WORKDIR "/src/Dotbot.API"
RUN dotnet build "Dotbot.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dotbot.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dotbot.API.dll"]
