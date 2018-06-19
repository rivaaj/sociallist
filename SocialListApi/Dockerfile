FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY SocialListApi/SocialListApi.csproj SocialListApi/
RUN dotnet restore SocialListApi/SocialListApi.csproj
COPY . .
WORKDIR /src/SocialListApi
RUN dotnet build SocialListApi.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish SocialListApi.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SocialListApi.dll"]
