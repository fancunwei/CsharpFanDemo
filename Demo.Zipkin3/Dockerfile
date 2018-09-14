FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 54876
EXPOSE 44309

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY Demo.Ziplin3/Demo.Ziplin3.csproj Demo.Ziplin3/
RUN dotnet restore Demo.Ziplin3/Demo.Ziplin3.csproj
COPY . .
WORKDIR /src/Demo.Ziplin3
RUN dotnet build Demo.Ziplin3.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Demo.Ziplin3.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Demo.Ziplin3.dll"]
