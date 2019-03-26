namespace TodoList.UnitTests
{
    using System.Linq;
    using System;
    using TodoList.Core.Boundaries.Do;
    using TodoList.Core.Entities;
    using TodoList.Core.Exceptions;
    using TodoList.Core.Gateways.InMemory;
    using TodoList.Core.Gateways;
    using TodoList.Core.UseCases;
    using TodoList.Core;
    using Xunit;

    public sealed class DoUseCaseTestsß
    {
        string existingTodoItemId = "3b35f11e-7080-45e2-a152-afff5a325508";

        [Fact]
        public void MarkItemCompletedSuccess()
        {
            InMemoryContext inMemory = new InMemoryContext();
            IItemGateway gateway = new ItemGateway(inMemory);
            IUseCase sut = new Do(gateway);

            sut.Execute(existingTodoItemId);

            Assert.NotEmpty(inMemory.Items.Where(e => e.Id == new Guid(existingTodoItemId) && e.Done));
        }

        [Fact]
        public void MarkItemCompleted_ThrowsException_WhenItemDoesNotExist()
        {
            InMemoryContext inMemory = new InMemoryContext();
            IItemGateway gateway = new ItemGateway(inMemory);
            IUseCase sut = new Do(gateway);

            Exception ex = Record.Exception(() => sut.Execute(Guid.NewGuid().ToString()));

            Assert.NotNull(ex);
            Assert.IsType<BusinessException>(ex);
        }
    }
}