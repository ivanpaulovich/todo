namespace TodoList.UnitTests
{
    using System;
    using TodoList.Core.Gateways;
    using TodoList.Core.UseCases.AddTodoItem;
    using TodoList.Core.UseCases;
    using TodoList.Core;
    using Xunit;
    using TodoList.Core.Gateways.InMemory;
    using System.Linq;

    public sealed class AddTodoItemTests
    {
        [Fact]
        public void GivenNullInput_ThrowsException()
        {
            IUseCase<Input> addTodoItem = new Interactor(null, null);
            Assert.Throws<Exception>(() => addTodoItem.Execute(null));
        }

        [Fact]
        public void GivenNullTitle_ThrowsException()
        {
            var input = new Core.UseCases.AddTodoItem.Input(null);
            IUseCase<Input> addTodoItem = new Interactor(null, null);
            Assert.Throws<Exception>(() => addTodoItem.Execute(input));
        }

        [Fact]
        public void GivenEmptyTitle_ThrowsException()
        {
            var input = new Core.UseCases.AddTodoItem.Input(string.Empty);
            IUseCase<Input> addTodoItem = new Interactor(null, null);
            Assert.Throws<Exception>(() => addTodoItem.Execute(input));
        }

        [Fact]
        public void GivenNotNullTitle_InvokeOutputHandler()
        {
            var context = new DBContext();
            var gateway = new TodoItemGateway(context);
            var outputHandler = new OutputHandler();
            Input input = new Input("My Title");
            IUseCase<Input> addTodoItem = new Interactor(outputHandler, gateway);
            addTodoItem.Execute(input);
            Assert.Equal(1, outputHandler.AddTodoItems.Count);
        }

        [Fact]
        public void GivenNotNullTitle_ReturnsId()
        {
            var context = new DBContext();
            var gateway = new TodoItemGateway(context);
            var outputHandler = new OutputHandler();
            Input input = new Input("My Title");
            IUseCase<Input> addTodoItem = new Interactor(outputHandler, gateway);
            addTodoItem.Execute(input);
            Assert.True(Guid.Empty != outputHandler.AddTodoItems[0].Id);
        }
    }
}