# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore the dependencies
COPY *.csproj .
RUN dotnet restore

# Copy the rest of the application code and build the application
COPY . .
RUN dotnet publish -c Release -o /app

# Stage 2: Run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .

# Expose port and start the application
EXPOSE 80
ENTRYPOINT ["dotnet", "YourAppName.dll"]
