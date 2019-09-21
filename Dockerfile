FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5000

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src

COPY ./TibberRobot.Domain /src/TibberRobot.Domain
COPY ./TibberRobot.Entities /src/TibberRobot.Entities
COPY ./TibberRobot.Repository /src/TibberRobot.Repository
COPY ./TibberRobot.API /src/TibberRobot.API

WORKDIR "/src/TibberRobot.API"
RUN dotnet restore "TibberRobot.API.csproj"

RUN dotnet build "TibberRobot.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TibberRobot.API.csproj" -c Release -o /app

COPY ./entrypoint.sh /
RUN sed -i 's/\r//' /entrypoint.sh
RUN chmod +x /entrypoint.sh

 
WORKDIR /app
 
CMD /entrypoint.sh

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TibberRobot.API.dll"]