namespace TodoList.UnitTests
{
    using System;
    using Xunit;
    using TodoList.Core;
    using TodoList.Core.UseCases.AddTask;
    using TodoList.Core.UseCases;
    using Moq;

    public class TaskTests
    {
        [Fact]
        public void AddTask_ThrowsException_WhenNullInput()
        {
            IUseCase addTask = new Interactor(null);
            Assert.Throws<Exception>(() => addTask.Execute(null));
        }

        [Fact]
        public void AddTask_ThrowsException_WhenNullTitle()
        {
            Builder builder = new Builder();
            IUseCase addTask = new Interactor(null);
            Assert.Throws<Exception>(() => addTask.Execute(builder.Build()));
        }

        [Fact]
        public void AddTask_ThrowsException_WhenEmptyTitle()
        {
            Builder builder = new Builder();
            builder.WithTitle(string.Empty);
            IUseCase addTask = new Interactor(null);
            Assert.Throws<Exception>(() => addTask.Execute(builder.Build()));
        }
    }
}
