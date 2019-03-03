namespace TodoList.UnitTests
{
    using System;
    using Xunit;
    using TodoList.Core;
    using TodoList.Core.UseCases.UpdateTitle;
    using TodoList.Core.UseCases;
    using Moq;

    public sealed class UpdateTitleTests
    {
        [Fact]
        public void GivenNullInput_ThrowsException()
        {
            IUseCase<Input> addTask = new Interactor();

            Assert.Throws<Exception>(() => addTask.Execute(null));
        }

        [Fact]
        public void GivenNullTitle_ThrowsException()
        {
            InputBuilder builder = new InputBuilder();
            IUseCase<Input> addTask = new Interactor();

            Assert.Throws<Exception>(() => addTask.Execute(builder.Build()));
        }

        [Fact]
        public void GivenEmptyTitle_ThrowsException()
        {
            InputBuilder builder = new InputBuilder();
            builder.WithTitle(string.Empty);
            IUseCase<Input> addTask = new Interactor();

            Assert.Throws<Exception>(() => addTask.Execute(builder.Build()));
        }

        [Fact]
        public void GivenNotNullTitle_DoesNotThrowException()
        {
            InputBuilder builder = new InputBuilder();
            builder.WithTitle("My Title");
            IUseCase<Input> addTask = new Interactor();

            Exception ex = Record.Exception(() => addTask.Execute(builder.Build()));
            Assert.Null(ex);
        }
    }
}