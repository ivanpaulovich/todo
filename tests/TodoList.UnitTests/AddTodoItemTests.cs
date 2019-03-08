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
            var addTodoItem = new Interactor(null, null, null);
            Assert.Throws<Exception>(() => addTodoItem.Execute(null));
        }

        [Fact]
        public void GivenNullTitle_ThrowsException()
        {
            var input = new Core.UseCases.AddTodoItem.AddTodoItemRequest(null);
            var addTodoItem = new Interactor(null, null, null);
            Assert.Throws<Exception>(() => addTodoItem.Execute(input));
        }

        [Fact]
        public void GivenEmptyTitle_ThrowsException()
        {
            var input = new Core.UseCases.AddTodoItem.AddTodoItemRequest(string.Empty);
            var addTodoItem = new Interactor(null, null, null);
            Assert.Throws<Exception>(() => addTodoItem.Execute(input));
        }

        [Fact]
        public void GivenNotNullTitle_InvokeOutputHandler()
        {
            var context = new DBContext();
            var gateway = new TodoItemGateway(context);
            var outputHandler = new OutputHandler();
            var entitiesFactory = new EntitiesFactory();

            var input = new AddTodoItemRequest("My Title");
            var addTodoItem = new Interactor(outputHandler, gateway, entitiesFactory);
            addTodoItem.Execute(input);
            Assert.Single(outputHandler.AddTodoItems);
        }

        [Fact]
        public void GivenNotNullTitle_ReturnsId()
        {
            var context = new DBContext();
            var gateway = new TodoItemGateway(context);
            var outputHandler = new OutputHandler();
            var entitiesFactory = new EntitiesFactory();

            var input = new AddTodoItemRequest("My Title");
            var addTodoItem = new Interactor(outputHandler, gateway, entitiesFactory);
            addTodoItem.Execute(input);
            Assert.True(Guid.Empty != outputHandler.AddTodoItems[0].Id);
        }
    }
}