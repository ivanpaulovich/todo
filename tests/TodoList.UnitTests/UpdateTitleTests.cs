namespace TodoList.UnitTests
{
    using System;
    using TodoList.Core.Gateways.InMemory;
    using TodoList.Core.Gateways;
    using TodoList.Core.UseCases.UpdateTitle;
    using TodoList.Core.UseCases;
    using TodoList.Core;
    using Xunit;
    using TodoList.Core.Entities;

    public sealed class UpdateTitleTests
    {
        [Fact]
        public void GivenNullInput_ThrowsException()
        {
            IUseCase<Input> updatedTitle = new Interactor(null);
            Assert.Throws<Exception>(() => updatedTitle.Execute(null));
        }

        [Fact]
        public void GivenNullTitle_ThrowsException()
        {
            Input input = new Input(Guid.Empty, null);
            IUseCase<Input> updatedTitle = new Interactor(null);
            Assert.Throws<Exception>(() => updatedTitle.Execute(input));
        }

        [Fact]
        public void GivenEmptyTitle_ThrowsException()
        {
            Input input = new Input(Guid.Empty, string.Empty);
            IUseCase<Input> updatedTitle = new Interactor(null);
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
            addTodoItem.Execute(new Core.UseCases.AddTodoItem.Input("My Title"));

            Input input = new Input(outputHandler.AddTodoItems[0].Id, "New Title");
            IUseCase<Input> updatedTitle = new Interactor(gateway);
            Exception ex = Record.Exception(() => updatedTitle.Execute(input));
            Assert.Null(ex);
        }
    }
}