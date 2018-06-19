FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY SocialList/SocialList.csproj SocialList/
RUN dotnet restore SocialList/SocialList.csproj
COPY . .
WORKDIR /src/SocialList
RUN dotnet build SocialList.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish SocialList.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SocialList.dll"]
