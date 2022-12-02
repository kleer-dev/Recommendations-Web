FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

RUN curl -fsSL https://deb.nodesource.com/setup_14.x | bash - \
    && apt-get install -y \
        nodejs \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /src
COPY ["Recommendations.Web/Recommendations.Web.csproj", "Recommendations.Web/"]
RUN dotnet restore "Recommendations.Web/Recommendations.Web.csproj"
COPY . .
WORKDIR "/src/Recommendations.Web"
RUN dotnet build "Recommendations.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Recommendations.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Recommendations.Web.dll"]
