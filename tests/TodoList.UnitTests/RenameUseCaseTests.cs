namespace TodoList.UnitTests
{
    using System;
    using TodoList.Core.Boundaries.Rename;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways.InMemory;
    using TodoList.Core.Gateways;
    using TodoList.Core.UseCases;
    using TodoList.Core;
    using Xunit;

    public sealed class RenameUseCaseTests
    {
        [Fact]
        public void GivenNullInput_ThrowsException()
        {
            var rename = new Rename(null);
            Assert.Throws<Exception>(() => rename.Execute(null));
        }

        [Fact]
        public void GivenNullTitle_ThrowsException()
        {
            var request = new Request(Guid.Empty.ToString(), null);
            var rename = new Rename(null);
            Assert.Throws<Exception>(() => rename.Execute(request));
        }

        [Fact]
        public void GivenEmptyTitle_ThrowsException()
        {
            var request = new Request(Guid.Empty.ToString(), string.Empty);
            var rename = new Rename(null);
            Assert.Throws<Exception>(() => rename.Execute(request));
        }

        [Fact]
        public void GivenTodoItem_TitleChanged()
        {
            var context = new InMemoryContext();
            var gateway = new ItemGateway(context);
            var responseHandler = new ResponseHandler();
            var entitiesFactory = new EntitiesFactory();

            var todo = new Core.UseCases.Todo(responseHandler, gateway, entitiesFactory);
            todo.Execute(new Core.Boundaries.Todo.Request("My Title"));

            var request = new Request(responseHandler.TodoItems[0].ItemId.ToString(), "New Title");
            var updatedTitle = new Rename(gateway);
            Exception ex = Record.Exception(() => updatedTitle.Execute(request));
            Assert.Null(ex);
        }
    }
}