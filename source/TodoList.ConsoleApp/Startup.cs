namespace TodoList.ConsoleApp
{
    using System;
    using TodoList.Core.Boundaries;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways.InMemory;
    using TodoList.Core.Gateways;
    using TodoList.Core.UseCases;
    using TodoList.Core;

    public sealed class Startup
    {
        private IUseCase<Core.Boundaries.Todo.Request> _todoUseCase;
        private TodoList.Core.Boundaries.Remove.IUseCase _removeUseCase;
        private TodoList.Core.Boundaries.List.IUseCase _listUseCase;
        private IUseCase<Core.Boundaries.Rename.Request> _renameUseCase;
        private TodoList.Core.Boundaries.Do.IUseCase _doUseCase;
        private TodoList.Core.Boundaries.Undo.IUseCase _undoUseCase;

        public Startup(
            IUseCase<Core.Boundaries.Todo.Request> todoUseCase,
            TodoList.Core.Boundaries.Remove.IUseCase removeUseCase,
            TodoList.Core.Boundaries.List.IUseCase listUseCase,
            IUseCase<TodoList.Core.Boundaries.Rename.Request> renameUseCase,
            TodoList.Core.Boundaries.Do.IUseCase doUseCase,
            TodoList.Core.Boundaries.Undo.IUseCase undoUseCase)
        {
            _todoUseCase = todoUseCase;
            _removeUseCase = removeUseCase;
            _listUseCase = listUseCase;
            _renameUseCase = renameUseCase;
            _doUseCase = doUseCase;
            _undoUseCase = undoUseCase;
        }

        internal void Rename(string[] args, string line)
        {
            if (args.Length < 3)
                return;

            string id = args[1];

            int firstSeparatorIndex = line.IndexOf(' ');
            if (firstSeparatorIndex <= 0)
                return;

            int secondSeparatorIndex = line.IndexOf(' ', firstSeparatorIndex);
            if (secondSeparatorIndex <= 0)
                return;

            string title = line.Substring(secondSeparatorIndex + 1);

            var input = new TodoList.Core.Boundaries.Rename.Request(id, title);
            _renameUseCase.Execute(input);
        }

        internal void List()
        {
            _listUseCase.Execute();
        }

        internal void Remove(string[] args)
        {
            if (args.Length != 2)
                return;

            _removeUseCase.Execute(args[1]);
        }

        internal void Todo(string line)
        {
            int separatorIndex = line.IndexOf(' ');

            if (separatorIndex <= 0)
                return;

            string title = line.Substring(separatorIndex + 1);

            var input = new TodoList.Core.Boundaries.Todo.Request(title);
            _todoUseCase.Execute(input);
        }

        internal void Undo(string[] args)
        {
            if (args.Length != 2)
                return;

            _undoUseCase.Execute(args[1]);
        }

        internal void Do(string[] args)
        {
            if (args.Length != 2)
                return;

            _doUseCase.Execute(args[1]);
        }
    }
}