FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 62617
EXPOSE 44386

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY Demo.OpenTracing/Demo.OpenTracing.csproj Demo.OpenTracing/
RUN dotnet restore Demo.OpenTracing/Demo.OpenTracing.csproj
COPY . .
WORKDIR /src/Demo.OpenTracing
RUN dotnet build Demo.OpenTracing.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Demo.OpenTracing.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Demo.OpenTracing.dll"]
