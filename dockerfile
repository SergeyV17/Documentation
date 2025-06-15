FROM flyway/flyway:latest AS flyway

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . .

RUN dotnet publish "src/VerticalSliceApi/" -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /app

RUN apt-get update && \
    apt-get install -y wget && \
    wget https://download.oracle.com/java/21/latest/jdk-21_linux-x64_bin.deb && \
    dpkg -i jdk-21_linux-x64_bin.deb && \
    apt-get clean && \
    rm jdk-21_linux-x64_bin.deb

COPY --from=flyway /flyway /flyway
COPY ./src/VerticalSliceApi.Migrations/flyway.conf /flyway/conf/
COPY ./src/VerticalSliceApi.Migrations/migrations/ /flyway/migrations/
COPY --from=build /app/out .
EXPOSE 5000
ENTRYPOINT ["sh", "-c", "/flyway/flyway repair && /flyway/flyway migrate && dotnet VerticalSliceApi.dll --urls=http://0.0.0.0:5000/"]