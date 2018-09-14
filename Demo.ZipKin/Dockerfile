FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 54800
EXPOSE 44383

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY Demo.ZipKin/Demo.ZipKin.csproj Demo.ZipKin/
RUN dotnet restore Demo.ZipKin/Demo.ZipKin.csproj
COPY . .
WORKDIR /src/Demo.ZipKin
RUN dotnet build Demo.ZipKin.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Demo.ZipKin.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Demo.ZipKin.dll"]
