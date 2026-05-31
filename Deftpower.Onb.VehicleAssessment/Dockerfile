FROM mcr.microsoft.com/dotnet/sdk:8.0

WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Development

EXPOSE 8080

COPY . .

RUN dotnet restore

ENTRYPOINT ["dotnet", "run", "--project", "Deftpower.Onb.VehicleAssessment.csproj", "--urls", "http://0.0.0.0:8080"]