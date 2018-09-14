FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 56611
EXPOSE 44353

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY Demo.ZipKinWeb/Demo.ZipKinWeb.csproj Demo.ZipKinWeb/
RUN dotnet restore Demo.ZipKinWeb/Demo.ZipKinWeb.csproj
COPY . .
WORKDIR /src/Demo.ZipKinWeb
RUN dotnet build Demo.ZipKinWeb.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Demo.ZipKinWeb.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Demo.ZipKinWeb.dll"]
