FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine3.15 AS build

COPY . /kuro-desserts
WORKDIR /src
RUN cp /kuro-desserts/kuro-desserts.csproj .
RUN dotnet restore kuro-desserts.csproj

RUN cp -r /kuro-desserts .
WORKDIR /src/kuro-desserts
RUN dotnet build kuro-desserts.csproj -c Release -o /app/build
RUN dotnet publish kuro-desserts.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine3.15

ENV JWT_KEY=d3c9e23120f8849f9e7f8132fbe5400757440493ae11789bfeacc5eabba33e95
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV ASPNETCORE_URLS=http://+:5000

RUN apk -U upgrade && \
apk add --no-cache \
icu-libs

EXPOSE 5000
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "kuro-desserts.dll"]