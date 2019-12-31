FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS build 

ENV ASPNETCORE_ENVIRONMENT=development

WORKDIR /app

COPY City-Informer.City.General.Api/src/City.General.Api/City.General.Api.csproj City.General.Api/
COPY City-Informer.City.Common/src/City.Common/City.Common.csproj City.Common/

RUN dotnet restore City.General.Api/City.General.Api.csproj

COPY City-Informer.City.General.Api/src/City.General.Api/.  City.General.Api/
COPY City-Informer.City.Common/src/City.Common/.  City.Common/

RUN dotnet publish City.General.Api/City.General.Api.csproj -c Release -o /app/out


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS runtime

WORKDIR /app
COPY --from=build /app/out .

ENV ASPNETCORE_URLS http://+:8003
EXPOSE 8003 

ENTRYPOINT [ "dotnet", "City.General.Api.dll" ]

