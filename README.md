# :white_check_mark: Commad-Line Task management with storage on your GitHub :fire:
<a href="https://www.nuget.org/packages/todo/" rel="todo">![NuGet](https://buildstats.info/nuget/todo)</a> [![Build status](https://ci.appveyor.com/api/projects/status/so416rowstopr46r/branch/master?svg=true)](https://ci.appveyor.com/project/ivanpaulovich/todo/branch/master)

The simple task management powered by .NET Core Global Tools and accesible everywhere with your GitHub account.

## Demo

![todo](https://github.com/ivanpaulovich/todo/raw/master/todo-v1.gif "todo")

## Install

```
$ dotnet tool install -g todo
```

## Setup

It requires a [Personal Access Token](https://github.com/settings/tokens) from your GitHub account. Create one, make sure to include `Gist` in the scope and copy the token. 

Replace the `YOUR_GIST_TOKEN` with the copied token then run the following command:

```
todo gt YOUR_GIST_TOKEN
```

## Task Management Commands

| Command  |  Description |
|---|---|
| `todo "Boil water in a large pot"`  |  Adds a new task. |
| `todo ls`  |  List all tasks. |
| `todo ren 128 "Salt the water"` |  Renames task title with id `128` to `Salt the water`. |
| `todo do 6d` | Marks task with id `6d` to done. |
| `todo undo f1381d68` | Marks task with id `f1381d68` to incomplete. |
| `todo rm f02a57b8` | Removes task with id `f02a57b8`. |

## Configuration Commands

| Command  |  Description |
|---|---|
| `todo gt YOUR_GIST_TOKEN` | Set the GitHub account ID. |
| `todo gi YOUR_GIST_ID` | Set the Gist ID to sync your tasks. |

Run `todo help` for the complete list of parameters and `todo i` to enter in the interactive mode.

## Development

### :arrow_forward: Running from source

```
$ dotnet run --project "source/TodoList.ConsoleApp/TodoList.ConsoleApp.csproj" -- i
```

### :checkered_flag: Development Environment

* MacOS Mojave :apple:
* Visual Studio Code :heart:
* [.NET Core SDK 2.2](https://www.microsoft.com/net/download/dotnet-core/2.2)
* Docker :whale: (Optional)
* SQL Server (Optional)

### :telephone: Support and Issues

Please give it a `star` then open an issue.
