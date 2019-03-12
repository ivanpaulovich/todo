# :cyclone: Clean Architecture Implementation Sample with .NET Core
[![Play with Docker](https://raw.githubusercontent.com/play-with-docker/stacks/master/assets/images/button.png)](https://labs.play-with-docker.com/?stack=https://raw.githubusercontent.com/ivanpaulovich/dotnet-clean-architecture/master/source/docker-compose.yml&stack_name=dotnet-clean-architecture) [![Build status](https://ci.appveyor.com/api/projects/status/005aoqa8g79skmn6/branch/master?svg=true)](https://ci.appveyor.com/project/ivanpaulovich/dotnet-clean-architecture/branch/master) [![Docker Cloud Automated Build](https://img.shields.io/docker/cloud/automated/ivanpaulovich/dotnet-clean-architecture.svg?style=plastic)](https://cloud.docker.com/repository/docker/ivanpaulovich/dotnet-clean-architecture)

After reading the great books from Robert C. Martin and watching his talks on youtube I was challenged to implement a Clean Architecture sample. To acomplish this goal, I selected the simple domain of a **Todo List app** then I started to design the solution following the principles found in the the book. If you share the same interest you are on the right place.

## Use Cases

The application is designed around the uses cases of a Todo List app. The user can do the following: 

* Add a todo item.
* List the todo items.
* Update the todo item titles.
* Complete a todo item.

## Project Organisation

The solution is divided between `Production Code` and `Unit Tests`. For the `Production Code` I created the following four layers:

* The Core project with Use Cases, Entities and Boundary Objects.
* The Infrastructure with the Data Access implementation.
* The Console App as the initial delivery mechanism.
* The Web API as the second delivery mechanism I implemented.

On the `Unit Tests` you will code to ensure the use cases business.

## Implementation Guide

A few excerpts from Clean Architecture book that will help you.

### The Dependency Rule

> Source code dependencies must point only inward, toward higher-level policies.

### The Stable Dependencies Principle

> Modules that are intended to be easy to change are not depended on by modules that are harder to change.

### The Stable Abstractions Principle

> A component should be as abstract as it is stable.

### Entities

> These entities are the business objects of the application. They encapsulate the most general and high-level rules

### Use Cases

> The software in the use cases layer contains application-specific business rules.

### Frameworks and Drivers

The frameworks and drivers layer is where all the details go. The web is a detail. The database is a detail. We keep these things on the outside where they can do little harm.

### User Interface

> Interface adapters layer is a set of adapters that convert data from the format most convenient for the use cases and entities, to the format most convenient for some external agency such as the database or the web.

#### Presenter Objects

The Presenter is the testable object. Its job is to accept data from the application and format it for presentation so that the View can simply move it to the scree.

## :zap: Running

You can run the application uses cases from the `Tests`, `Console` or from the `Web API`. We are continuously delivering the [Todo List Swagger Demo on Google Cloud](http://35.188.17.14/index.html).

### Unit Tests

```
dotnet test tests/TodoList.UnitTests/TodoList.UnitTests.csproj
```

### Console Demo (InMemory Persistence)

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

### Web API (InMemory Persistence)

```
dotnet run --project "source/TodoList.WebApi/TodoList.WebApi.csproj"
```

### Web API (SQL Server Persistence)

When you change the environment to `Production` then the `StartupProduction` class will be instantiated which uses the SQL Server persistence.

```
dotnet run --project --environment="production" "source/TodoList.WebApi/TodoList.WebApi.csproj"
```

Then navigate to `https://localhost:5001/`.

## :floppy_disk: Setup SQL Server (Optional)

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
