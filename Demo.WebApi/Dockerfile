FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 51809
EXPOSE 44313

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY Demo.WebApi/Demo.WebApi.csproj Demo.WebApi/
RUN dotnet restore Demo.WebApi/Demo.WebApi.csproj
COPY . .
WORKDIR /src/Demo.WebApi
RUN dotnet build Demo.WebApi.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Demo.WebApi.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Demo.WebApi.dll"]
