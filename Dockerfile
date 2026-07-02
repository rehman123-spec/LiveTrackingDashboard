# Use the official .NET 10 SDK as a build image
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["LiveTrackingDashboard.csproj", "./"]
RUN dotnet restore "LiveTrackingDashboard.csproj"

# Copy the rest of the application
COPY . .
RUN dotnet publish "LiveTrackingDashboard.csproj" -c Release -o /app/publish

# Use the runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expose port 8080 (Render uses this port)
EXPOSE 8080

# Set the environment variable for the port
ENV ASPNETCORE_URLS=http://+:8080

# Run the application
ENTRYPOINT ["dotnet", "LiveTrackingDashboard.dll"]
