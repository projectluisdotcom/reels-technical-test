FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

COPY *.sln .
COPY App/*.csproj ./App/
COPY Domain/*.csproj ./Domain/
COPY Data/*.csproj ./Data/
COPY UnitTests/*.csproj ./UnitTests/

RUN dotnet restore

COPY App/. ./App/
COPY Domain/. ./Domain/
COPY Data/. ./Data/
COPY UnitTests/. ./UnitTests/

RUN dotnet test -c Release

WORKDIR /app/App
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app/App/out .
ENTRYPOINT ["dotnet", "App.dll"]