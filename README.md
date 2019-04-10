# :white_check_mark: An extensible Todo List app in your terminal :fire:
<a href="https://www.nuget.org/packages/todo/" rel="todo">![NuGet](https://buildstats.info/nuget/todo)</a> [![Build status](https://ci.appveyor.com/api/projects/status/so416rowstopr46r/branch/master?svg=true)](https://ci.appveyor.com/project/ivanpaulovich/todo/branch/master)

The simple, powerfull and extensible Todo List app in your terminal powered by .NET Core Global Tools.

## Install

```
$ dotnet tool install --global todo
```

## Usage

| Command  |  Description |
|---|---|
| `todo "Boil water in a large pot"`  |  Adds a task. |
| `todo ls`  |  Enlist the tasks. |
| `todo ren 128 "Salt the water"` |  Renames task title with `128` to `Salt the water`. |
| `todo do 6d` | Marks task with id `6d` to done. |
| `todo undo f1381d68` | Marks task with id `f1381d68` to incomplete. |
| `todo rm f02a57b8` | Removes task with id `f02a57b8`. |
| `todo i` | Enters interactive mode. |
| `todo help` | For complete list os parameters. |

## Demo

```
$ todo ls
1286e19b [ ] Salt the water with at least a tablespoon
6dc154c1 [ ] Add pasta
692394f8 [ ] Stir the pasta
f1381d68 [ ] Test the pasta by tasting it
f02a57b8 [ ] Drain the pasta
2cebc5cb [X] Boil water in a large pot
```

### Running from source

```
$ dotnet run --project "source/TodoList.ConsoleApp/TodoList.ConsoleApp.csproj" -- i 
```

## :checkered_flag: Development Environment

* MacOS Sierra
* VSCode :heart:
* [.NET Core SDK 2.2](https://www.microsoft.com/net/download/dotnet-core/2.2).
* Docker :whale:
* SQL Server.

## :telephone: Support and Issues

Please open an issue. Leave a star if you like the project!