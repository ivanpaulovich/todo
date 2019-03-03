namespace TodoList.UnitTests {
    using System;
    using Moq;
    using TodoList.Core.UseCases.UpdateTitle;
    using TodoList.Core.UseCases;
    using TodoList.Core;
    using Xunit;

    public sealed class UpdateTitleTests {
        [Fact]
        public void GivenNullInput_ThrowsException () {
            IUseCase<Input> updatedTitle = new Interactor ();

            Assert.Throws<Exception> (() => updatedTitle.Execute (null));
        }

        [Fact]
        public void GivenNullTitle_ThrowsException () {
            InputBuilder builder = new InputBuilder ();
            IUseCase<Input> updatedTitle = new Interactor ();

            Assert.Throws<Exception> (() => updatedTitle.Execute (builder.Build ()));
        }

        [Fact]
        public void GivenEmptyTitle_ThrowsException () {
            InputBuilder builder = new InputBuilder ();
            builder.WithTitle (string.Empty);
            IUseCase<Input> updatedTitle = new Interactor ();

            Assert.Throws<Exception> (() => updatedTitle.Execute (builder.Build ()));
        }

        [Fact]
        public void GivenNotNullTitle_DoesNotThrowException () {
            InputBuilder builder = new InputBuilder ();
            builder
                .WithTodoItemId (Guid.NewGuid ())
                .WithTitle ("My Title");

            IUseCase<Input> updatedTitle = new Interactor ();

            var ex = Record.Exception (() => updatedTitle.Execute (builder.Build ()));

            Assert.Null (ex);
        }

        [Fact]
        public void GivenTodoItem_TitleChanged () {
            Core.UseCases.AddTodoItem.Output actualOutput = null;

            var outputHandler = new Mock<Core.UseCases.AddTodoItem.IOutputHandler> ();
            outputHandler.Setup (e => e.Handle (It.IsAny<Core.UseCases.AddTodoItem.Output> ()))
                .Callback<Core.UseCases.AddTodoItem.Output> (output => actualOutput = output);

            var AddTodoItemBuilder = new Core.UseCases.AddTodoItem.InputBuilder ();
            AddTodoItemBuilder.WithTitle ("My Title");
            var AddTodoItem = new Core.UseCases.AddTodoItem.Interactor (outputHandler.Object);

            AddTodoItem.Execute (AddTodoItemBuilder.Build ());

            Guid TodoItemId = Guid.NewGuid ();

            InputBuilder builder = new InputBuilder ();
            builder
                .WithTodoItemId (actualOutput.Id)
                .WithTitle ("New Title");

            IUseCase<Input> updatedTitle = new Interactor ();

            Exception ex = Record.Exception (() => updatedTitle.Execute (builder.Build ()));

            Assert.Null (ex);
        }
    }
}