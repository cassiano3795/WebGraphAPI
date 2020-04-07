FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /
COPY ["WebGraphAPI/WebGraphAPI/WebGraphAPI.csproj", "WebGraphAPI/"]
COPY ["WebGraphAPI/ServiceCollections/ServiceCollections.csproj", "ServiceCollections/"]
COPY ["WebGraphAPI/JwtSecurity/JwtSecurity.csproj", "JwtSecurity/"]
COPY ["WebGraphAPI/CoreGraphQL/CoreGraphQL.csproj", "CoreGraphQL/"]
COPY ["WebGraphAPI/BD/BD.csproj", "BD/"]
COPY ["WebGraphAPI/Services/Services.csproj", "Services/"]
COPY ["WebGraphAPI/Core/Core.csproj", "Core/"]
COPY ["WebGraphAPI/Repositories/Repositories.csproj", "Repositories/"]
COPY ["WebGraphAPI/WebGraphAPI/wwwroot/Schemas", "wwwroot/schemas"]
RUN dotnet restore "WebGraphAPI/WebGraphAPI.csproj"
COPY . .
WORKDIR "/WebGraphAPI"
RUN dotnet build "WebGraphAPI/WebGraphAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebGraphAPI/WebGraphAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=publish/wwwroot/schemas /app/wwwroot/schemas .
ENTRYPOINT ["dotnet", "WebGraphAPI.dll"]