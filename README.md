
# Unit Conversion API

A RESTful ASP.NET Core Web API for converting numerical values between units of measurement.

## Features

- Length conversions: metres, kilometres, centimetres, millimetres, miles, feet and inches.
- Temperature conversions: Celsius, Fahrenheit and Kelvin.
- Weight/mass conversions: kilograms, grams, milligrams, pounds and ounces.
- Input validation and HTTP 400 error responses.
- Swagger/OpenAPI documentation.
- Centralized unit definitions for future extensibility.

## Technology Stack

- C#
- ASP.NET Core Web API
- .NET 10 LTS
- Swagger / OpenAPI

## Prerequisites

- .NET 10 SDK

## Run Locally

1. Clone the repository:

   ```bash
   git clone https://github.com/YOUR-USERNAME/unit-conversion-api.git
   ```

2. Navigate to the project directory:

   ```bash
   cd unit-conversion-api/UnitConversionApi
   ```

3. Restore dependencies:

   ```bash
   dotnet restore
   ```

4. Build:

   ```bash
   dotnet build
   ```

5. Run:

   ```bash
   dotnet run --urls http://localhost:5000
   ```

6. Open Swagger UI:

   http://localhost:5000/swagger

Replace YOUR-USERNAME with your GitHub username.

## API Endpoints

### Convert a value

`GET /api/conversions/convert`

Parameters: `value`, `from`, `to`

Example:

`/api/conversions/convert?value=100&from=m&to=ft`

### List supported units

`GET /api/conversions/units`

Returns the supported units and their categories.

## Design Decisions

- Conversion logic is separated from the controller through a service interface.
- Each unit is defined by its category, conversion factor and optional offset.
- Values are converted through a canonical base unit.
- Temperature conversions support offsets.
- Unsupported units and incompatible categories return HTTP 400 responses.
- Units are hardcoded for this version; a database-backed catalog could support a larger system.

## Future Improvements

- Automated unit and integration tests.
- Additional measurement categories.
- Structured logging and centralized exception handling.
- GitHub Actions CI pipeline.
