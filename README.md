# :cyclone: Clean Architecture Sample with .NET Core

An example of the classic Clean Architecture from Uncle Bob.

# :zap: Running Unit Tests

```
dotnet test tests/TodoList.UnitTests/TodoList.UnitTests.csproj
```

# :zap: Running Console Demo

```
dotnet run --project "source/TodoList.ConsoleApp/TodoList.ConsoleApp.csproj"
Usage:
        add [title]
        finish [id]
        list
        update [id] [title]
        exit
```

## :floppy_disk: [Optional] Running on SQL Server

### Setup SQL Server in Docker

Run `scripts/sql-docker-up.sh` to setup a SQL Server in a Docker container with the following Connection String:

```
Server=localhost;User Id=sa;Password=<YourNewStrong!Passw0rd>;
```

#### Update the Database

Generate tables and seed the database via Entity Framework Tool:

```sh
dotnet ef database update --project source/TodoList.Infrastructure --startup-project source/TodoList.WebApi
```

#### Add Migration

Run the EF Tool to add a migration to the `TodoList.Infrastructure` project.

```sh
dotnet ef migrations add "InitialCreate" -o "EntityFrameworkDataAccess/Migrations" --project source/TodoList.Infrastructure --startup-project source/TodoList.WebApi
```

## :checkered_flag: Developer Environment

* MacOS Sierra
* VSCode :heart:
* [.NET Core SDK 2.2](https://www.microsoft.com/net/download/dotnet-core/2.2).

## :telephone: Support and Issues

Please open an issue and I am happy to answer. Give the project a :star: if you like the code.
