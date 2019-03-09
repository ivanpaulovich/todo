# :cyclone: Clean Architecture Sample with .NET Core

An example of the classic Clean Architecture from Uncle Bob.

# :zap: Running Unit Tests

```
dotnet test tests/TodoList.UnitTests/TodoList.UnitTests.csproj
```

# :zap: Running Demos

Running the **Console Demo** which is configured to use InMemory persistence.

```
dotnet run --project "source/TodoList.ConsoleApp/TodoList.ConsoleApp.csproj"

```

```
Usage:
        add [title]
        finish [id]
        list
        update [id] [title]
        exit
```

Running the **Web API** which is configured to use SQL Server persistence.

```
dotnet run --project "source/TodoList.WebApi/TodoList.WebApi.csproj"
```

## :floppy_disk: Running on SQL Server (Optional)

### Setup SQL Server in Docker

Run `scripts/sql-docker-up.sh` to setup a SQL Server in a Docker container with the following Connection String:

```
Server=localhost;User Id=sa;Password=<YourNewStrong!Passw0rd>;
```

#### Add Migration

Run the EF Tool to add a migration to the `TodoList.Infrastructure` project.

```sh
dotnet ef migrations add "InitialCreate" -o "EntityFrameworkDataAccess/Migrations" --project source/TodoList.Infrastructure --startup-project source/TodoList.WebApi
```

#### Update the Database

Generate tables and seed the database via Entity Framework Tool:

```sh
dotnet ef database update --project source/TodoList.Infrastructure --startup-project source/TodoList.WebApi
```

## :checkered_flag: Development Environment

* MacOS Sierra
* VSCode :heart:
* [.NET Core SDK 2.2](https://www.microsoft.com/net/download/dotnet-core/2.2).
* Docker :whale:
* SQL Server.

## :telephone: Support and Issues

I am happy to answer issues. Give a :star: if you like the project.