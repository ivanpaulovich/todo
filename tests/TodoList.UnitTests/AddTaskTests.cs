namespace TodoList.UnitTests
{
    using System;
    using Xunit;
    using TodoList.Core;
    using TodoList.Core.UseCases.AddTask;
    using TodoList.Core.UseCases;
    using Moq;

    public sealed class AddTaskTests
    {
        [Fact]
        public void GivenNullInput_ThrowsException()
        {
            IUseCase<Input> addTask = new Interactor(null);

            Assert.Throws<Exception>(() => addTask.Execute(null));
        }

        [Fact]
        public void GivenNullTitle_ThrowsException()
        {
            InputBuilder builder = new InputBuilder();
            IUseCase<Input> addTask = new Interactor(null);

            Assert.Throws<Exception>(() => addTask.Execute(builder.Build()));
        }

        [Fact]
        public void GivenEmptyTitle_ThrowsException()
        {
            InputBuilder builder = new InputBuilder();
            builder.WithTitle(string.Empty);
            IUseCase<Input> addTask = new Interactor(null);

            Assert.Throws<Exception>(() => addTask.Execute(builder.Build()));
        }

        [Fact]
        public void GivenNotNullTitle_InvokeOutputHandler()
        {
            var outputHandler = new Mock<IOutputHandler>();
            InputBuilder builder = new InputBuilder();
            builder.WithTitle("My Title");
            IUseCase<Input> addTask = new Interactor(outputHandler.Object);

            addTask.Execute(builder.Build());

            outputHandler.Verify(e => e.Handle(It.IsAny<Output>()), Times.Once);
        }

        [Fact]
        public void GivenNotNullTitle_ReturnsId()
        {
            Output actualOutput = null;
            var outputHandler = new Mock<IOutputHandler>();
            outputHandler.Setup(e => e.Handle(It.IsAny<Output>()))
                .Callback<Output>(output => actualOutput = output);
            InputBuilder builder = new InputBuilder();
            builder.WithTitle("My Title");
            IUseCase<Input> addTask = new Interactor(outputHandler.Object);

            addTask.Execute(builder.Build());

            Assert.True(actualOutput.Id != Guid.Empty);
        }
    }
}
