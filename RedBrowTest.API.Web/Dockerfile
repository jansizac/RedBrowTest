FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RedBrowTest.API.Web/RedBrowTest.API.Web.csproj", "RedBrowTest.API.Web/"]
COPY ["RedBrowTest.Core.Application/RedBrowTest.Core.Application.csproj", "RedBrowTest.Core.Application/"]
COPY ["RedBrowTest.Core.Domain/RedBrowTest.Core.Domain.csproj", "RedBrowTest.Core.Domain/"]
COPY ["RedBrowTest.Infrastructure.Identity/RedBrowTest.Infrastructure.Identity.csproj", "RedBrowTest.Infrastructure.Identity/"]
COPY ["RedBrowTest.Infrastructure.MSSQL/RedBrowTest.Infrastructure.MSSQL.csproj", "RedBrowTest.Infrastructure.MSSQL/"]
RUN dotnet restore "RedBrowTest.API.Web/RedBrowTest.API.Web.csproj"
COPY . .
WORKDIR "/src/RedBrowTest.API.Web"
RUN dotnet build "RedBrowTest.API.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RedBrowTest.API.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RedBrowTest.API.Web.dll"]