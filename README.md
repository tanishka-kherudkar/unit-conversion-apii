# Unit Conversion API

A RESTful ASP.NET Core Web API for converting length, temperature, and weight units.

## Features

- Length conversions: metres, kilometres, centimetres, millimetres, miles, feet, and inches.
- Temperature conversions: Celsius, Fahrenheit, and Kelvin.
- Weight conversions: kilograms, grams, milligrams, pounds, and ounces.
- Input validation and HTTP 400 error responses for invalid conversions.
- Swagger UI for interactive API documentation and testing.
- Automated unit tests using xUnit.

## Technology Stack

- C#
- ASP.NET Core Web API
- .NET 10
- Swagger / OpenAPI
- xUnit

## Prerequisites

Install the .NET 10 SDK.

## Run the API

Clone the repository:

```bash
git clone https://github.com/YOUR-USERNAME/unit-conversion-api.git
cd unit-conversion-api/UnitConversionApi
```

Restore dependencies and build:

```bash
dotnet restore
dotnet build
```

Run the API:

```bash
dotnet run
```

Open the Swagger URL shown in the terminal. The default local URL may vary based on the launch configuration.

## API Endpoints

### Convert units

`GET /api/Conversions/convert?value=100&from=m&to=ft`

Example response:

```json
{
  "originalValue": 100,
  "fromUnit": "m",
  "toUnit": "ft",
  "convertedValue": 328.0833333333333,
  "category": "length"
}
```

### Get supported units

`GET /api/Conversions/units`

### Error handling

Invalid units, incompatible conversion categories, and temperatures below absolute zero return HTTP 400 Bad Request.

## Run Tests

From the repository root:

```bash
dotnet test
```

## Design Decisions

- Conversion logic is separated into a service to keep controllers lightweight.
- Each unit is converted through a common base unit for its category.
- Temperature conversions use both a factor and an offset.
- Dependency injection supports maintainability and testability.
- Invalid inputs are rejected with meaningful error messages.

## Possible Future Improvements

- Add more units and conversion categories.
- Add integration tests for HTTP endpoints.
- Improve validation and API versioning.
