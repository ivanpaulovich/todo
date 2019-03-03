namespace TodoList.UnitTests {
    using System;
    using Moq;
    using TodoList.Core.Gateways.InMemory;
    using TodoList.Core.Gateways;
    using TodoList.Core.UseCases.UpdateTitle;
    using TodoList.Core.UseCases;
    using TodoList.Core;
    using Xunit;

    public sealed class UpdateTitleTests {
        [Fact]
        public void GivenNullInput_ThrowsException () {
            IUseCase<Input> updatedTitle = new Interactor (null);

            Assert.Throws<Exception> (() => updatedTitle.Execute (null));
        }

        [Fact]
        public void GivenNullTitle_ThrowsException () {
            InputBuilder builder = new InputBuilder ();
            IUseCase<Input> updatedTitle = new Interactor (null);

            Assert.Throws<Exception> (() => updatedTitle.Execute (builder.Build ()));
        }

        [Fact]
        public void GivenEmptyTitle_ThrowsException () {
            InputBuilder builder = new InputBuilder ();
            builder.WithTitle (string.Empty);
            IUseCase<Input> updatedTitle = new Interactor (null);

            Assert.Throws<Exception> (() => updatedTitle.Execute (builder.Build ()));
        }

        [Fact]
        public void GivenTodoItem_TitleChanged () {
            var context = new DBContext ();
            var gateway = new TodoItemGateway (context);

            Core.UseCases.AddTodoItem.Output actualOutput = null;

            var outputHandler = new Mock<Core.UseCases.IUseCaseOutputHandler<Core.UseCases.AddTodoItem.Output>> ();
            outputHandler.Setup (e => e.Handle (It.IsAny<Core.UseCases.AddTodoItem.Output> ()))
                .Callback<Core.UseCases.AddTodoItem.Output> (output => actualOutput = output);

            var addTodoItemBuilder = new Core.UseCases.AddTodoItem.InputBuilder ();
            addTodoItemBuilder.WithTitle ("My Title");
            var addTodoItem = new Core.UseCases.AddTodoItem.Interactor (outputHandler.Object, gateway);

            addTodoItem.Execute (addTodoItemBuilder.Build ());

            InputBuilder builder = new InputBuilder ();
            builder
                .WithTodoItemId (actualOutput.Id)
                .WithTitle ("New Title");

            IUseCase<Input> updatedTitle = new Interactor (gateway);

            Exception ex = Record.Exception (() => updatedTitle.Execute (builder.Build ()));

            Assert.Null (ex);
        }
    }
}