FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-bionic AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-bionic AS build
WORKDIR /src
COPY ["WebGraphAPI/WebGraphAPI.csproj", "WebGraphAPI/"]
COPY ["ServiceCollections/ServiceCollections.csproj", "ServiceCollections/"]
COPY ["JwtSecurity/JwtSecurity.csproj", "JwtSecurity/"]
COPY ["CoreGraphQL/CoreGraphQL.csproj", "CoreGraphQL/"]
COPY ["BD/BD.csproj", "BD/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["Repositories/Repositories.csproj", "Repositories/"]
RUN dotnet restore "WebGraphAPI/WebGraphAPI.csproj"
COPY . .
WORKDIR "/src/WebGraphAPI"
RUN dotnet build "WebGraphAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebGraphAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebGraphAPI.dll"]