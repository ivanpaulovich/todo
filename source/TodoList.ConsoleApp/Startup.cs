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
        private IUseCase<Core.Boundaries.AddTodoItem.Request> _addTodoItem;
        private TodoList.Core.Boundaries.RemoveTodoItem.IUseCase _removeTodoItem;
        private TodoList.Core.Boundaries.ListTodoItems.IUseCase _listTodoItems;
        private IUseCase<Core.Boundaries.UpdateTitle.Request> _updateTitle;
        private TodoList.Core.Boundaries.MarkItemCompleted.IUseCase _markItemCompleted;
        private TodoList.Core.Boundaries.MarkItemIncomplete.IUseCase _markItemIncomplete;


        public Startup(
            IUseCase<Core.Boundaries.AddTodoItem.Request> addTodoItem,
            TodoList.Core.Boundaries.RemoveTodoItem.IUseCase removeTodoItem,
            TodoList.Core.Boundaries.ListTodoItems.IUseCase listTodoItems,
            IUseCase<TodoList.Core.Boundaries.UpdateTitle.Request> updateTitle,
            TodoList.Core.Boundaries.MarkItemCompleted.IUseCase markItemCompleted,
            TodoList.Core.Boundaries.MarkItemIncomplete.IUseCase markItemIncomplete)
        {
            _addTodoItem = addTodoItem;
            _removeTodoItem = removeTodoItem;
            _listTodoItems = listTodoItems;
            _updateTitle = updateTitle;
            _markItemCompleted = markItemCompleted;
            _markItemIncomplete = markItemIncomplete;
        }

        internal void UpdateTodoItem(string[] args, string line)
        {
            if (args.Length < 3)
                return;

            Guid id;

            if (!Guid.TryParse(args[1], out id))
                return;

            int firstSeparatorIndex = line.IndexOf(' ');
            if (firstSeparatorIndex <= 0)
                return;

            int secondSeparatorIndex = line.IndexOf(' ', firstSeparatorIndex);
            if (secondSeparatorIndex <= 0)
                return;
            
            string title = line.Substring(secondSeparatorIndex + 1);

            var input = new TodoList.Core.Boundaries.UpdateTitle.Request(id, title);
            _updateTitle.Execute(input);
        }

        internal void ListTodoItem()
        {
            _listTodoItems.Execute();
        }

        internal void RemoveTodoItem(string[] args)
        {
            if (args.Length != 2)
                return;

            _removeTodoItem.Execute(new Guid(args[1]));
        }

        internal void AddTodoItem(string line)
        {
            int separatorIndex = line.IndexOf(' ');
            
            if (separatorIndex <= 0)
                return;
            
            string title = line.Substring(separatorIndex + 1);

            var input = new TodoList.Core.Boundaries.AddTodoItem.Request(title);
            _addTodoItem.Execute(input);
        }

        internal void IncompleteTodoItem(string[] args)
        {
            if (args.Length != 2)
                return;

            _markItemIncomplete.Execute(new Guid(args[1]));
        }

        internal void CompleteTodoItem(string[] args)
        {
            if (args.Length != 2)
                return;

            _markItemCompleted.Execute(new Guid(args[1]));
        }
    }
}