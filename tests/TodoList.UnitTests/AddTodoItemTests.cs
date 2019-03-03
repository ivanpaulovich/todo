namespace TodoList.UnitTests {
    using System;
    using Moq;
    using TodoList.Core.UseCases.AddTodoItem;
    using TodoList.Core.UseCases;
    using TodoList.Core;
    using Xunit;

    public sealed class AddTodoItemTests {
        [Fact]
        public void GivenNullInput_ThrowsException () {
            IUseCase<Input> AddTodoItem = new Interactor (null);

            Assert.Throws<Exception> (() => AddTodoItem.Execute (null));
        }

        [Fact]
        public void GivenNullTitle_ThrowsException () {
            InputBuilder builder = new InputBuilder ();
            IUseCase<Input> AddTodoItem = new Interactor (null);

            Assert.Throws<Exception> (() => AddTodoItem.Execute (builder.Build ()));
        }

        [Fact]
        public void GivenEmptyTitle_ThrowsException () {
            InputBuilder builder = new InputBuilder ();
            builder.WithTitle (string.Empty);
            IUseCase<Input> AddTodoItem = new Interactor (null);

            Assert.Throws<Exception> (() => AddTodoItem.Execute (builder.Build ()));
        }

        [Fact]
        public void GivenNotNullTitle_InvokeOutputHandler () {
            var outputHandler = new Mock<IOutputHandler> ();
            InputBuilder builder = new InputBuilder ();
            builder.WithTitle ("My Title");
            IUseCase<Input> AddTodoItem = new Interactor (outputHandler.Object);

            AddTodoItem.Execute (builder.Build ());

            outputHandler.Verify (e => e.Handle (It.IsAny<Output> ()), Times.Once);
        }

        [Fact]
        public void GivenNotNullTitle_ReturnsId () {
            Output actualOutput = null;
            var outputHandler = new Mock<IOutputHandler> ();
            outputHandler.Setup (e => e.Handle (It.IsAny<Output> ()))
                .Callback<Output> (output => actualOutput = output);
            InputBuilder builder = new InputBuilder ();
            builder.WithTitle ("My Title");
            IUseCase<Input> AddTodoItem = new Interactor (outputHandler.Object);

            AddTodoItem.Execute (builder.Build ());

            Assert.True (actualOutput.Id != Guid.Empty);
        }
    }
}