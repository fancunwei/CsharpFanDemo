FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 56633
EXPOSE 44369

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY Demo.ZipKinWeb2/Demo.ZipKinWeb2.csproj Demo.ZipKinWeb2/
RUN dotnet restore Demo.ZipKinWeb2/Demo.ZipKinWeb2.csproj
COPY . .
WORKDIR /src/Demo.ZipKinWeb2
RUN dotnet build Demo.ZipKinWeb2.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Demo.ZipKinWeb2.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Demo.ZipKinWeb2.dll"]
