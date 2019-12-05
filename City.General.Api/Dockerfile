FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS build 

WORKDIR /app

COPY src/City.General.Api.csproj GeneralApi/
RUN dotnet restore GeneralApi/City.General.Api.csproj

COPY src/. GeneralApi/  
RUN dotnet publish GeneralApi/City.General.Api.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS runtime

ENV WeatherApiUrl http://localhost:[nr]

WORKDIR /app
COPY --from=build /app/out .

EXPOSE 8003
ENTRYPOINT [ "dotnet", "City.General.Api.dll" ]

