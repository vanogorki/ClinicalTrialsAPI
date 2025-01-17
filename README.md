# ClinicalTrials

## Overview
ClinicalTrials is a C# project designed to manage and analyze clinical trial data. The project is structured into several components, including domain, application, persistence, API, and integration tests.

## Project Structure
- **ClinicalTrials.Domain**: Contains the core domain models.
- **ClinicalTrials.Application**: Contains application services and use cases.
- **ClinicalTrials.Persistence**: Handles data access and storage.
- **ClinicalTrials.API**: Exposes the application functionality through a RESTful API.
- **ClinicalTrials.IntegrationTests**: Contains integration tests to ensure the components work together correctly.

## Prerequisites
- Docker
- .NET 8.0 SDK (optional)
- IDE (e.g., JetBrains Rider) (optional)

## Getting Started
1. Clone the repository:

   ```bash
   git clone https://github.com/vanogorki/ClinicalTrials.git
   ```
2. Navigate to the project directory:

   ```bash
   cd ClinicalTrials
   ```

3. Build and run:

    - Docker:

      ```bash
      docker-compose up --build
      ```

    - Local: Update the connection string in `appsettings.Development.json` (if needed)

      and run:

      ```bash
      dotnet run --project ./src/ClinicalTrials.API/ClinicalTrials.API.csproj
      ```
      
4. Open a browser and navigate to `http://localhost:5000/swagger` to view the API documentation.

## Running Tests
To run the integration tests, use the following command:
```sh
dotnet test tests/ClinicalTrials.IntegrationTests/ClinicalTrials.IntegrationTests.csproj
