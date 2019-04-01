namespace TodoList.ConsoleApp.Controllers
{
    using TodoList.ConsoleApp.Commands;
    using TodoList.Core.Boundaries;

    public class TodoItemsController
    {
        private IUseCase<Core.Boundaries.Todo.Request> _todoUseCase;
        private Core.Boundaries.Remove.IUseCase _removeUseCase;
        private Core.Boundaries.List.IUseCase _listUseCase;
        private IUseCase<Core.Boundaries.Rename.Request> _renameUseCase;
        private Core.Boundaries.Do.IUseCase _doUseCase;
        private Core.Boundaries.Undo.IUseCase _undoUseCase;
        private Presenter _presenter;

        public TodoItemsController(
            IUseCase<Core.Boundaries.Todo.Request> todoUseCase,
            Core.Boundaries.Remove.IUseCase removeUseCase,
            Core.Boundaries.List.IUseCase listUseCase,
            IUseCase<Core.Boundaries.Rename.Request> renameUseCase,
            Core.Boundaries.Do.IUseCase doUseCase,
            Core.Boundaries.Undo.IUseCase undoUseCase,
            Presenter presenter)
        {
            _todoUseCase = todoUseCase;
            _removeUseCase = removeUseCase;
            _listUseCase = listUseCase;
            _renameUseCase = renameUseCase;
            _doUseCase = doUseCase;
            _undoUseCase = undoUseCase;
            _presenter = presenter;
        }

        public void Execute(TodoCommand todoCommand)
        {
            var request = new TodoList.Core.Boundaries.Todo.Request(todoCommand.Title);
            _todoUseCase.Execute(request);
        }

        public void Execute(RemoveCommand removeCommand)
        {
            _removeUseCase.Execute(removeCommand.Id);
        }

        public void Execute(ListCommand listCommand)
        {
            _listUseCase.Execute();
        }

        public void Execute(RenameCommand renameCommand)
        {
            var request = new TodoList.Core.Boundaries.Rename.Request(renameCommand.Id, renameCommand.NewTitle);
            _renameUseCase.Execute(request);
        }

        public void Execute(DoCommand doCommand)
        {
            _doUseCase.Execute(doCommand.Id);
        }

        public void Execute(UndoCommand undoCommand)
        {
            _undoUseCase.Execute(undoCommand.Id);
        }

        public void Execute(HelpCommand helpCommand)
        {
            _presenter.DisplayInstructions();
        }
    }
}