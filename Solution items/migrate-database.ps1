
dotnet build "..\"
dotnet-fm migrate -p Postgres -c "User ID=postgres;Password=mysecretpassword;Host=localhost;Port=5432;Database=weather-app;" -a ".\..\src\WebApi\bin\Debug\net7.0\Infrastructure.dll" --allowDirtyAssemblies