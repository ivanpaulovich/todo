namespace TodoList.ConsoleApp.Controllers
{
    using System.IO;
    using System.Reflection;
    using Colorful;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using TodoList.ConsoleApp.Commands;
    using TodoList.Core.Boundaries;

    public sealed class TodoItemsController
    {
        private IUseCase<Core.Boundaries.Todo.Request> todoUseCase;
        private Core.Boundaries.Remove.IUseCase removeUseCase;
        private Core.Boundaries.List.IUseCase listUseCase;
        private IUseCase<Core.Boundaries.Rename.Request> renameUseCase;
        private Core.Boundaries.Do.IUseCase doUseCase;
        private Core.Boundaries.Undo.IUseCase undoUseCase;
        public bool IsInteractive { get; private set; }

        public TodoItemsController(
            IUseCase<Core.Boundaries.Todo.Request> todoUseCase,
            Core.Boundaries.Remove.IUseCase removeUseCase,
            Core.Boundaries.List.IUseCase listUseCase,
            IUseCase<Core.Boundaries.Rename.Request> renameUseCase,
            Core.Boundaries.Do.IUseCase doUseCase,
            Core.Boundaries.Undo.IUseCase undoUseCase)
        {
            this.todoUseCase = todoUseCase;
            this.removeUseCase = removeUseCase;
            this.listUseCase = listUseCase;
            this.renameUseCase = renameUseCase;
            this.doUseCase = doUseCase;
            this.undoUseCase = undoUseCase;
        }

        public void Execute(TodoCommand todoCommand)
        {
            var request = new Core.Boundaries.Todo.Request(todoCommand.Title);
            todoUseCase.Execute(request);
        }

        public void Execute(RemoveCommand removeCommand)
        {
            removeUseCase.Execute(removeCommand.Id);
        }

        public void List()
        {
            listUseCase.Execute();
        }

        public void Execute(RenameCommand renameCommand)
        {
            var request = new Core.Boundaries.Rename.Request(renameCommand.Id, renameCommand.NewTitle);
            renameUseCase.Execute(request);
        }

        public void Execute(DoCommand doCommand)
        {
            doUseCase.Execute(doCommand.Id);
        }

        public void Execute(UndoCommand undoCommand)
        {
            undoUseCase.Execute(undoCommand.Id);
        }

        public void Help()
        {
            DisplayInstructions();
        }

        public void Interactive()
        {
            IsInteractive = true;
        }

        private void DisplayInstructions()
        {
            Console.WriteLine("The usage");
            Console.WriteLine("\ttodo [title]");
            Console.WriteLine("\ttodo ren [id] [title]");
            Console.WriteLine("\ttodo do [id]");
            Console.WriteLine("\ttodo undo [id]");
            Console.WriteLine("\ttodo ls");
            Console.WriteLine("\ttodo rm [id]");
            Console.WriteLine("\ttodo gt [Gist Token]");
            Console.WriteLine("\ttodo gi [Gist ID]");
            Console.WriteLine("\texit");
        }

        public void Execute(GistTokenCommand gistTokenCommand)
        {
            SetGistTokenSettings(gistTokenCommand.GistToken);
        }

        public void Execute(GistIdCommand gistTokenCommand)
        {
            SetGistIdSettings(gistTokenCommand.GistId);
        }

        private void SetGistTokenSettings(string gistToken)
        {
            var appsettingsPath = Path.GetDirectoryName(
                    Assembly.GetExecutingAssembly().Location) +
                    Path.DirectorySeparatorChar +
                    "appsettings.json";
            
            var contents = File.ReadAllText(appsettingsPath);
            var configuration = JsonConvert.DeserializeObject<JObject>(contents);

            configuration["GistToken"] = gistToken;

            contents = JsonConvert.SerializeObject(configuration, Formatting.Indented);
            File.WriteAllText(appsettingsPath, contents);
        }

        private void SetGistIdSettings(string gistId)
        {
            var appsettingsPath = Path.GetDirectoryName(
                    Assembly.GetExecutingAssembly().Location) +
                    Path.DirectorySeparatorChar +
                    "appsettings.json";
            
            var contents = File.ReadAllText(appsettingsPath);
            var configuration = JsonConvert.DeserializeObject<JObject>(contents);

            configuration["GistId"] = gistId;

            contents = JsonConvert.SerializeObject(configuration, Formatting.Indented);
            File.WriteAllText(appsettingsPath, contents);
        }

        public void DevelopmentMode()
        {
            var appsettingsPath = Path.GetDirectoryName(
                    Assembly.GetExecutingAssembly().Location) +
                    Path.DirectorySeparatorChar +
                    "appsettings.json";
            
            var contents = File.ReadAllText(appsettingsPath);
            var configuration = JsonConvert.DeserializeObject<JObject>(contents);

            configuration["Environment"] = "Development";

            contents = JsonConvert.SerializeObject(configuration, Formatting.Indented);
            File.WriteAllText(appsettingsPath, contents);
        }
    }
}