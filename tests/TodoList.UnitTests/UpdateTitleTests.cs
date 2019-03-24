namespace TodoList.UnitTests
{
    using System;
    using TodoList.Core.Boundaries.UpdateTitle;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways.InMemory;
    using TodoList.Core.Gateways;
    using TodoList.Core.UseCases;
    using TodoList.Core;
    using Xunit;

    public sealed class UpdateTitleTests
    {
        [Fact]
        public void GivenNullInput_ThrowsException()
        {
            var updatedTitle = new UpdateTitle(null);
            Assert.Throws<Exception>(() => updatedTitle.Execute(null));
        }

        [Fact]
        public void GivenNullTitle_ThrowsException()
        {
            var request = new Request(Guid.Empty, null);
            var updatedTitle = new UpdateTitle(null);
            Assert.Throws<Exception>(() => updatedTitle.Execute(request));
        }

        [Fact]
        public void GivenEmptyTitle_ThrowsException()
        {
            var request = new Request(Guid.Empty, string.Empty);
            var updatedTitle = new UpdateTitle(null);
            Assert.Throws<Exception>(() => updatedTitle.Execute(request));
        }

        [Fact]
        public void GivenTodoItem_TitleChanged()
        {
            var context = new InMemoryContext();
            var gateway = new TodoItemGateway(context);
            var responseHandler = new ResponseHandler();
            var entitiesFactory = new EntitiesFactory();

            var addTodoItem = new Core.UseCases.AddTodoItem(responseHandler, gateway, entitiesFactory);
            addTodoItem.Execute(new Core.Boundaries.AddTodoItem.Request("My Title"));

            var request = new Request(responseHandler.AddTodoItems[0].Id, "New Title");
            var updatedTitle = new UpdateTitle(gateway);
            Exception ex = Record.Exception(() => updatedTitle.Execute(request));
            Assert.Null(ex);
        }
    }
}