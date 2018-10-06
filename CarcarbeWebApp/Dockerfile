FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 51027
EXPOSE 44351

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["CarcarbeWebApp/CarcarbeWebApp.csproj", "CarcarbeWebApp/"]
RUN dotnet restore "CarcarbeWebApp/CarcarbeWebApp.csproj"
COPY . .
WORKDIR "/src/CarcarbeWebApp"
RUN dotnet build "CarcarbeWebApp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CarcarbeWebApp.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CarcarbeWebApp.dll"]