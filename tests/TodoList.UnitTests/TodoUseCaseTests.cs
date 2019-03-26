namespace TodoList.UnitTests
{
    using System.Linq;
    using System;
    using TodoList.Core.Boundaries.Todo;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways.InMemory;
    using TodoList.Core.Gateways;
    using TodoList.Core.UseCases;
    using TodoList.Core;
    using Xunit;

    public sealed class TodoUseCaseTests
    {
        [Fact]
        public void GivenNullInput_ThrowsException()
        {
            var todo = new Todo(null, null, null);
            Assert.Throws<Exception>(() => todo.Execute(null));
        }

        [Fact]
        public void GivenNullTitle_ThrowsException()
        {
            var request = new Core.Boundaries.Todo.Request(null);
            var todo = new Todo(null, null, null);
            Assert.Throws<Exception>(() => todo.Execute(request));
        }

        [Fact]
        public void GivenEmptyTitle_ThrowsException()
        {
            var request = new Request(string.Empty);
            var addTodoItem = new Todo(null, null, null);
            Assert.Throws<Exception>(() => addTodoItem.Execute(request));
        }

        [Fact]
        public void GivenTitle_ItemsWasAdded()
        {
            var context = new InMemoryContext();
            var gateway = new ItemGateway(context);
            var responseHandler = new ResponseHandler();
            var entitiesFactory = new EntitiesFactory();

            var request = new Request("My Title");
            var todo = new Todo(responseHandler, gateway, entitiesFactory);
            todo.Execute(request);
            Assert.Single(responseHandler.TodoItems);
        }

        [Fact]
        public void GivenTitle_ReturnsId()
        {
            var context = new InMemoryContext();
            var gateway = new ItemGateway(context);
            var responseHandler = new ResponseHandler();
            var entitiesFactory = new EntitiesFactory();

            var request = new Request("My Title");
            var todo = new Todo(responseHandler, gateway, entitiesFactory);
            todo.Execute(request);
            Assert.True(Guid.Empty != responseHandler.TodoItems[0].ItemId);
        }
    }
}