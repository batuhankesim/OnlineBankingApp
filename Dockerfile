# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files and restore dependencies
COPY ["OnlineBankingApp/OnlineBankingApp.csproj", "OnlineBankingApp/"]
COPY ["OnlineBankingApp.Entity/OnlineBankingApp.Entity.csproj", "../OnlineBankingApp.Entity/"]
COPY ["OnlineBankingApp.Service/OnlineBankingApp.Service.csproj", "../OnlineBankingApp.Service/"]
COPY ["OnlineBankingApp.Common/OnlineBankingApp.Common.csproj", "../OnlineBankingApp.Common/"]
RUN dotnet restore "OnlineBankingApp/OnlineBankingApp.csproj"

# Copy the rest of the application code
COPY . .

# Build and publish the application
WORKDIR "/src/OnlineBankingApp"
RUN dotnet build "OnlineBankingApp.csproj" -c Release -o /app
RUN dotnet publish "OnlineBankingApp.csproj" -c Release -o /app

# Stage 2: Run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .

# Expose port and start the application
EXPOSE 80
ENTRYPOINT ["dotnet", "OnlineBankingApp.dll"]
