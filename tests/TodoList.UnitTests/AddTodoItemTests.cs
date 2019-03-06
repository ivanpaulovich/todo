namespace TodoList.UnitTests
{
    using System.Linq;
    using System;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways.InMemory;
    using TodoList.Core.Gateways;
    using TodoList.Core.UseCases.AddTodoItem;
    using TodoList.Core.UseCases;
    using TodoList.Core;
    using Xunit;

    public sealed class AddTodoItemTests
    {
        [Fact]
        public void GivenNullInput_ThrowsException()
        {
            IUseCase<AddTodoItemRequest> addTodoItem = new Interactor(null, null, null);
            Assert.Throws<Exception>(() => addTodoItem.Execute(null));
        }

        [Fact]
        public void GivenNullTitle_ThrowsException()
        {
            var input = new Core.UseCases.AddTodoItem.AddTodoItemRequest(null);
            IUseCase<AddTodoItemRequest> addTodoItem = new Interactor(null, null, null);
            Assert.Throws<Exception>(() => addTodoItem.Execute(input));
        }

        [Fact]
        public void GivenEmptyTitle_ThrowsException()
        {
            var input = new Core.UseCases.AddTodoItem.AddTodoItemRequest(string.Empty);
            IUseCase<AddTodoItemRequest> addTodoItem = new Interactor(null, null, null);
            Assert.Throws<Exception>(() => addTodoItem.Execute(input));
        }

        [Fact]
        public void GivenNotNullTitle_InvokeOutputHandler()
        {
            var context = new DBContext();
            var gateway = new TodoItemGateway(context);
            var outputHandler = new OutputHandler();
            var entitiesFactory = new EntitiesFactory();

            AddTodoItemRequest input = new AddTodoItemRequest("My Title");
            IUseCase<AddTodoItemRequest> addTodoItem = new Interactor(outputHandler, gateway, entitiesFactory);
            addTodoItem.Execute(input);
            Assert.Equal(1, outputHandler.AddTodoItems.Count);
        }

        [Fact]
        public void GivenNotNullTitle_ReturnsId()
        {
            var context = new DBContext();
            var gateway = new TodoItemGateway(context);
            var outputHandler = new OutputHandler();
            var entitiesFactory = new EntitiesFactory();

            AddTodoItemRequest input = new AddTodoItemRequest("My Title");
            IUseCase<AddTodoItemRequest> addTodoItem = new Interactor(outputHandler, gateway, entitiesFactory);
            addTodoItem.Execute(input);
            Assert.True(Guid.Empty != outputHandler.AddTodoItems[0].Id);
        }
    }
}