FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-bionic AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2.104-sdk AS build
WORKDIR /src
RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -
RUN apt-get install -y nodejs
COPY ["CarcarbeWebApp/CarcarbeWebApp.csproj", "CarcarbeWebApp/"]
RUN dotnet restore "CarcarbeWebApp/CarcarbeWebApp.csproj"
COPY . .
WORKDIR "/src/CarcarbeWebApp"
RUN dotnet build "CarcarbeWebApp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CarcarbeWebApp.csproj" -c Release --no-restore -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
#CMD ["dotnet", "CarcarbeWebApp.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet CarcarbeWebApp.dll