FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["LocadoraVeiculos.API/LocadoraVeiculos.API.csproj", "LocadoraVeiculos.API/"]
COPY ["LocadoraVeiculos.Application/LocadoraVeiculos.Application.csproj", "LocadoraVeiculos.Application/"]
COPY ["LocadoraVeiculos.Domain/LocadoraVeiculos.Domain.csproj", "LocadoraVeiculos.Domain/"]
COPY ["LocadoraVeiculos.Entities/LocadoraVeiculos.Entities.csproj", "LocadoraVeiculos.Entities/"]
COPY ["LocadoraVeiculos.Infrastructure/LocadoraVeiculos.Infrastructure.csproj", "LocadoraVeiculos.Infrastructure/"]

RUN dotnet restore "LocadoraVeiculos.API/LocadoraVeiculos.API.csproj"

COPY . .
WORKDIR "/src/LocadoraVeiculos.API"
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

USER root
RUN mkdir -p /app/data && chown -R $APP_UID:$APP_UID /app/data
USER $APP_UID

ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "LocadoraVeiculos.API.dll"]