namespace TodoList.UnitTests
{
    using System.Linq;
    using System;
    using TodoList.Core.Boundaries.AddTodoItem;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways.InMemory;
    using TodoList.Core.Gateways;
    using TodoList.Core.UseCases;
    using TodoList.Core;
    using Xunit;

    public sealed class AddTodoItemTests
    {
        [Fact]
        public void GivenNullInput_ThrowsException()
        {
            var addTodoItem = new AddTodoItem(null, null, null);
            Assert.Throws<Exception>(() => addTodoItem.Execute(null));
        }

        [Fact]
        public void GivenNullTitle_ThrowsException()
        {
            var request = new Core.Boundaries.AddTodoItem.Request(null);
            var addTodoItem = new AddTodoItem(null, null, null);
            Assert.Throws<Exception>(() => addTodoItem.Execute(request));
        }

        [Fact]
        public void GivenEmptyTitle_ThrowsException()
        {
            var request = new Request(string.Empty);
            var addTodoItem = new AddTodoItem(null, null, null);
            Assert.Throws<Exception>(() => addTodoItem.Execute(request));
        }

        [Fact]
        public void GivenTitle_ItemsWasAdded()
        {
            var context = new InMemoryContext();
            var gateway = new TodoItemGateway(context);
            var responseHandler = new ResponseHandler();
            var entitiesFactory = new EntitiesFactory();

            var request = new Request("My Title");
            var addTodoItem = new AddTodoItem(responseHandler, gateway, entitiesFactory);
            addTodoItem.Execute(request);
            Assert.Single(responseHandler.AddTodoItems);
        }

        [Fact]
        public void GivenTitle_ReturnsId()
        {
            var context = new InMemoryContext();
            var gateway = new TodoItemGateway(context);
            var responseHandler = new ResponseHandler();
            var entitiesFactory = new EntitiesFactory();

            var request = new Request("My Title");
            var addTodoItem = new AddTodoItem(responseHandler, gateway, entitiesFactory);
            addTodoItem.Execute(request);
            Assert.True(Guid.Empty != responseHandler.AddTodoItems[0].Id);
        }
    }
}