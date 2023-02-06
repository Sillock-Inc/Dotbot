FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Dotbot.Discord/Dotbot.Discord.csproj", "Dotbot.Discord/"]
RUN dotnet restore "Dotbot.Discord/Dotbot.Discord.csproj"
COPY . .
WORKDIR "/src/Dotbot.Discord"
RUN dotnet build "Dotbot.Discord.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dotbot.Discord.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dotbot.Discord.dll"]
