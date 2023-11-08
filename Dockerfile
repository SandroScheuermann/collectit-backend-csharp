# Use the official .NET Core SDK as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app 

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
# Copy the project file and restore any dependencies (use .csproj for the project name)

COPY /Muscler.API/Muscler.API.csproj ./Muscler.API/Muscler.API.csproj
COPY /Muscler.Domain/Muscler.Domain.csproj ./Muscler.Domain/Muscler.Domain.csproj
COPY /Muscler.Infra/Muscler.Infra.csproj ./Muscler.Infra/Muscler.Infra.csproj
COPY /Muscler.Tests/Muscler.Tests.csproj ./Muscler.Tests/Muscler.Tests.csproj
COPY Muscler.sln ./ 

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
ENTRYPOINT ["dotnet", "Muscler.API.dll"] 