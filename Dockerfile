FROM microsoft/dotnet:1.0.0-preview1

WORKDIR /app
COPY . /app

RUN dotnet restore
RUN dotnet build

ENTRYPOINT dotnet run