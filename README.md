# :white_check_mark: An extensible Todo List app in your terminal :fire:
<a href="https://www.nuget.org/packages/todo/" rel="todo">![NuGet](https://buildstats.info/nuget/todo)</a> [![Build status](https://ci.appveyor.com/api/projects/status/so416rowstopr46r/branch/master?svg=true)](https://ci.appveyor.com/project/ivanpaulovich/todo/branch/master)

The simple, powerfull and extensible Todo List app in your terminal powered by .NET Core Global Tools.

## Demo

![todo](https://github.com/ivanpaulovich/todo/raw/master/todo.gif "todo")

## Install

```
$ dotnet tool install -g todo
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

### Running from source

```
$ dotnet run --project "source/TodoList.ConsoleApp/TodoList.ConsoleApp.csproj" -- i 
```

## :checkered_flag: Development Environment

* MacOS Mojave :apple:
* Visual Studio Code :heart:
* [.NET Core SDK 2.2](https://www.microsoft.com/net/download/dotnet-core/2.2)
* Docker :whale: (Optional)
* SQL Server (Optional)

## :telephone: Support and Issues

Please give it a `star` then open an issue.