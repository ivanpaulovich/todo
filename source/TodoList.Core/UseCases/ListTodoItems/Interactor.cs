namespace TodoList.Core.UseCases.ListTodoItems
{
    using System.Collections.Generic;
    using TodoList.Core.Gateways;

    public sealed class Interactor : IUseCase<Input>
    {
        private IOutputHandler<Output> _outputHandler;
        private ITodoItemGateway _todoItemGateway;

        public Interactor(
            IOutputHandler<Output> outputHandler,
            ITodoItemGateway todoItemGateway)
        {
            _outputHandler = outputHandler;
            _todoItemGateway = todoItemGateway;
        }

        public void Execute(Input input)
        {
            var todoItems = _todoItemGateway.List();
            Output output = CreateOutput(todoItems);
            _outputHandler.Handle(output);
        }

        private Output CreateOutput(IList<Entities.TodoItem> todoItems)
        {
            OutputBuilder output = new OutputBuilder();
            foreach (var item in todoItems)
                output.WithItem(item.Id, item.Title);

            return output.Build();
        }
    }
}