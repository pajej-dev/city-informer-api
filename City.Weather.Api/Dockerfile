FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS build 

WORKDIR /app

COPY src/City.Weather.Api.csproj WeatherApi/
RUN dotnet restore WeatherApi/City.Weather.Api.csproj

COPY src/. WeatherApi/  
RUN dotnet publish WeatherApi/City.Weather.Api.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS runtime

WORKDIR /app
COPY --from=build /app/out .

EXPOSE 8001
ENTRYPOINT [ "dotnet", "City.Weather.Api.dll" ]

