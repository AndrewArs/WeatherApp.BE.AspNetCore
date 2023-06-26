# WeatherApp.BE.AspNetCore
## About
Application written using *Clean Architecture*, *Railway Oriented Programming*, *AspNetCore WebApi*, *CQRS*. Contains examples of usage NuGet packages such as [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet), [fluentmigrator](https://github.com/fluentmigrator/fluentmigrator), [FluentValidation](https://github.com/FluentValidation/FluentValidation), [Humanizer](https://github.com/Humanizr/Humanizer), [MediatR](https://github.com/jbogard/MediatR), [serilog](https://github.com/serilog/serilog), [xunit](https://github.com/xunit/xunit)

## Dependencies
- Docker 
- Dotnet 7.0
- Postgres (`docker run --name postgres -e POSTGRES_PASSWORD=mysecretpassword -p 5432:5432 -d postgres`)
- dotnet-fm CLI tool for migrations (`dotnet tool install -g FluentMigrator.DotNet.Cli`)
----------

## To start the project
1. Configure all dependecies
2. Run migration script [migrate-database.ps1](/Solution%20items/migrate-database.ps1)
3. Run [WebApi](/src/WebApi/)
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
    
- **Solution items** - scripts/docker images/settings
    - **migrate-database.ps1** - script used to run migrations over Postgres database (contains hardcoded connection string which should be replaces with yours)
    
