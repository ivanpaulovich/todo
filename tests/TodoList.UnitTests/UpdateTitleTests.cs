namespace TodoList.UnitTests {
    using System;
    using Moq;
    using TodoList.Core.UseCases.UpdateTitle;
    using TodoList.Core.UseCases;
    using TodoList.Core;
    using Xunit;
    using TodoList.Core.Gateways;
    using TodoList.Core.Gateways.InMemory;

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
            var context = new Context();
            var gateway = new TodoItemGateway (context);

            Core.UseCases.AddTodoItem.Output actualOutput = null;

            var outputHandler = new Mock<Core.UseCases.AddTodoItem.IOutputHandler> ();
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