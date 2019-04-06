namespace TodoList.ConsoleApp.Controllers
{
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

        public void Execute(ListCommand listCommand)
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
    }
}