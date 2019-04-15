FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
#RUN apk add  --no-cache --repository http://dl-cdn.alpinelinux.org/alpine/v3.9/main/ nodejs=10.14.2-r0
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