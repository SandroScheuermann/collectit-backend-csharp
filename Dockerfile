# Stage 1: Construir a aplicação
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-api
WORKDIR /src
COPY ["YourWebAPIProject.csproj", "./"]
RUN dotnet restore "./CollectIt.API.csproj"
COPY . .
RUN dotnet publish "CollectIt.API.csproj" -c Release -o /app/publish

# Stage 2: Criar a imagem da Web API
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS publish-api
WORKDIR /app
COPY --from=build-api /app/publish .
ENTRYPOINT ["dotnet", "CollectIt.API.dll"]