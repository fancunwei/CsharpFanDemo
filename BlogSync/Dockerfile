FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 53396
EXPOSE 44363

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY BlogSYNC/BlogSYNC.csproj BlogSYNC/
RUN dotnet restore BlogSYNC/BlogSYNC.csproj
COPY . .
WORKDIR /src/BlogSYNC
RUN dotnet build BlogSYNC.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish BlogSYNC.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BlogSYNC.dll"]
