FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 51027
EXPOSE 44351
RUN apt-get update
#INSTALL PHYTON
RUN apt-get install -y python2.7
#INSTALL NODEJS
RUN apt-get install wget
RUN wget --no-check-certificate https://nodejs.org/dist/v9.3.0/node-v9.3.0-linux-x64.tar.gz  
RUN tar xzvf node-v9.3.0-linux-x64.tar.gz
RUN chown -Rv root.root node-v9.3.0-linux-x64
RUN cp -Rvi node-v9.3.0-linux-x64 /opt/node
RUN rm -r -f node-v9.3.0-linux-x64
RUN rm node-v9.3.0-linux-x64.tar.gz
ENV PATH="/opt/node/bin:$PATH"
RUN npm config set python /usr/bin/python2.7

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