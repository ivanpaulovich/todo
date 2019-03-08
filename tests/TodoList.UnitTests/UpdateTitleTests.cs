namespace TodoList.UnitTests
{
    using System;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways.InMemory;
    using TodoList.Core.Gateways;
    using TodoList.Core.UseCases.UpdateTitle;
    using TodoList.Core.UseCases;
    using TodoList.Core;
    using Xunit;

    public sealed class UpdateTitleTests
    {
        [Fact]
        public void GivenNullInput_ThrowsException()
        {
            var updatedTitle = new Interactor(null);
            Assert.Throws<Exception>(() => updatedTitle.Execute(null));
        }

        [Fact]
        public void GivenNullTitle_ThrowsException()
        {
            var input = new UpdateTitleRequest(Guid.Empty, null);
            var updatedTitle = new Interactor(null);
            Assert.Throws<Exception>(() => updatedTitle.Execute(input));
        }

        [Fact]
        public void GivenEmptyTitle_ThrowsException()
        {
            var input = new UpdateTitleRequest(Guid.Empty, string.Empty);
            var updatedTitle = new Interactor(null);
            Assert.Throws<Exception>(() => updatedTitle.Execute(input));
        }

        [Fact]
        public void GivenTodoItem_TitleChanged()
        {
            var context = new DBContext();
            var gateway = new TodoItemGateway(context);
            var outputHandler = new OutputHandler();
            var entitiesFactory = new EntitiesFactory();

            var addTodoItem = new Core.UseCases.AddTodoItem.Interactor(outputHandler, gateway, entitiesFactory);
            addTodoItem.Execute(new Core.UseCases.AddTodoItem.AddTodoItemRequest("My Title"));

            var input = new UpdateTitleRequest(outputHandler.AddTodoItems[0].Id, "New Title");
            var updatedTitle = new Interactor(gateway);
            Exception ex = Record.Exception(() => updatedTitle.Execute(input));
            Assert.Null(ex);
        }
    }
}