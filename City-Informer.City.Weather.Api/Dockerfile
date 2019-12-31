FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS build 

ENV ASPNETCORE_ENVIRONMENT=development

WORKDIR /app

COPY City-Informer.City.Weather.Api/src/City.Weather.Api/City.Weather.Api.csproj City.Weather.Api/
COPY City-Informer.City.Common/src/City.Common/City.Common.csproj City.Common/

RUN dotnet restore City.Weather.Api/City.Weather.Api.csproj

COPY City-Informer.City.Weather.Api/src/City.Weather.Api/.  City.Weather.Api/
COPY City-Informer.City.Common/src/City.Common/.  City.Common/

RUN dotnet publish City.Weather.Api/City.Weather.Api.csproj -c Release -o /app/out


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS runtime

WORKDIR /app
COPY --from=build /app/out .

ENV ASPNETCORE_URLS http://+:8001
EXPOSE 8001

ENTRYPOINT [ "dotnet", "City.Weather.Api.dll" ]

