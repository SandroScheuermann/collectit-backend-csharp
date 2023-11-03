# Use the official .NET Core SDK as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app 

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
# Copy the project file and restore any dependencies (use .csproj for the project name)

COPY /CollectIt.API/CollectIt.API.csproj ./CollectIt.API/CollectIt.API.csproj
COPY /CollectIt.Domain/CollectIt.Domain.csproj ./CollectIt.Domain/CollectIt.Domain.csproj
COPY /CollectIt.Infra/CollectIt.Infra.csproj ./CollectIt.Infra/CollectIt.Infra.csproj
COPY /CollectIt.Tests/CollectIt.Tests.csproj ./CollectIt.Tests/CollectIt.Tests.csproj
COPY CollectIt.sln ./ 

RUN dotnet restore CollectIt.sln

# Copy the rest of the application code
COPY . .

# Publish the application
RUN dotnet publish -c Release -o /app/out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./ 

# Start the application
ENTRYPOINT ["dotnet", "CollectIt.API.dll"] 