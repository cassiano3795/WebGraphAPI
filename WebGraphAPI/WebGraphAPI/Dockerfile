FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-bionic AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-bionic AS build
WORKDIR /src
COPY ["ZeengGraphAPI/ZeengGraphAPI.csproj", "ZeengGraphAPI/"]
COPY ["ZServiceCollections/ZServiceCollections.csproj", "ZServiceCollections/"]
COPY ["ZJwtSecurity/ZJwtSecurity.csproj", "ZJwtSecurity/"]
COPY ["ZGraphQL/ZGraphQL.csproj", "ZGraphQL/"]
COPY ["ZBD/ZBD.csproj", "ZBD/"]
COPY ["ZServices/ZServices.csproj", "ZServices/"]
COPY ["ZCore/ZCore.csproj", "ZCore/"]
COPY ["ZRepositories/ZRepositories.csproj", "ZRepositories/"]
RUN dotnet restore "ZeengGraphAPI/ZeengGraphAPI.csproj"
COPY . .
WORKDIR "/src/ZeengGraphAPI"
RUN dotnet build "ZeengGraphAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ZeengGraphAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ZeengGraphAPI.dll"]