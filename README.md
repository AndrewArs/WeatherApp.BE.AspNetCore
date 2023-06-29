# WeatherApp.BE.AspNetCore
## About app
This is a weather forecast app where users can add forecast providers and browse forecasts. The provider model consists of the following fields:
- **name**: The unique name of the provider.
- **url**: The URL to be executed for each forecast request.
- **temperature_path**: The JSON path to the temperature value. For example, 'data.[0].temperature_c' in the JSON structure '{ "data": [ {"temperature_c": 20.0} ] }'.
- **forecast_template_path**: The template to be built for the forecast request. It should consist of text with JSON path placeholders. For example, 'Expected temperature for today is {{data.[0].temperature_c}} C'.
- **key_query_param_name**: The parameter from the query string to be masked with '{secret}'. For example, 'http://api.weather.com?key=my_key' would be transformed to 'http://api.weather.com?key={secret}'.
For templating, use the symbols '{{}}' and for the JSON selector, use the syntax where the user selects the property or index of the array at each step. For example, if we have the following model:
```json
{
    "location": {
    "name": "Lisbon",
    "country": "Portugal"
    },
    "data" : [
        "current": {
            "temperature": 27,
            "weather_descriptions": [
                { "text": "Partly cloudy" },
                { "text": "Rainy somewhere" }
            ],
            "wind_speed": 20
        }
    ]
}
``` 
- selector for **"temperature": 27** would be `data.[0].current.temperature`
- selector for **"Partly cloudy"** would be `data.[0].current.weather_descriptions.[0].text`
- selector for **"Rainy somewhere"** would be `data.[0].current.weather_descriptions.[1].text`
- selector for **"name": "Lisbon"** would be `location.name`
- selector for **"country": "Portugal"** would be `location.country`

Example of **forecast template**:
Forecast for {{location.name}} {{location.country}}. Temperature expected for today is {{data.[0].current.temperature}} C. Conditions {{data.[0].current.weather_descriptions.[0].text}}, {{data.[0].current.weather_descriptions.[1].text}}.
----------

## About technologies used
Application written using *Clean Architecture*, *Railway Oriented Programming*, *AspNetCore WebApi*, *CQRS*, *Unit Testing*, *Integration Testing*. Contains examples of usage NuGet packages such as [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet), [fluentmigrator](https://github.com/fluentmigrator/fluentmigrator), [FluentValidation](https://github.com/FluentValidation/FluentValidation), [Humanizer](https://github.com/Humanizr/Humanizer), [MediatR](https://github.com/jbogard/MediatR), [serilog](https://github.com/serilog/serilog), [xunit](https://github.com/xunit/xunit), [fluentassertions](https://github.com/fluentassertions/fluentassertions), [Respawn](https://github.com/jbogard/Respawn), [Testcontainers](https://github.com/testcontainers/testcontainers-dotnet), [efcore](https://github.com/dotnet/efcore)
----------

## Dependencies
- Docker 
- Dotnet 7.0
- dotnet-fm CLI tool for migrations (`dotnet tool install -g FluentMigrator.DotNet.Cli`)
----------

## To start the project
1. Configure all dependecies
2. Go to [Solution items folder](/Solution%20items)
3. Run docker compose using script [start-docker-compose.bat](/Solution%20items/start-docker-compose.ps1) or execute this command in console `docker compose up -d --build`
4. Run migration PowerShell script [migrate-database.bat](/Solution%20items/migrate-database.ps1) or execute two commands in console
    ```console
    dotnet build "..\"
    dotnet-fm migrate -p Postgres -c "User ID=postgres Password=mysecretpassword;Host=localhost;Port=5432;Database=weather-app;" -a ".\..\src\WebApi\bin\Debug\net7.0\Infrastructure.dll" --allowDirtyAssemblies
    ```
5. Browse http://localhost:5000/swagger
----------

## Project structure
- **src** - source code of the application
    - **Application** - core project which contains all bussiness logic
    - **Domain** - database entities
    - **Infrastructure** - utility services, connectors to storage
    - **WebApi** - REST API entry point of application

- **tests** - project tests and benchmarks
    - **Benchmarks** - benchmarks of different versions of algorithms used in application
    - **UnitTests** - tests for utility services
    - **IntegrationTests** - controller tests
    
- **Solution items** - scripts/docker files/settings
    - **migrate-database.ps1** - script used to run migrations over Postgres database (contains hardcoded connection string which should be replaces with yours)
    
