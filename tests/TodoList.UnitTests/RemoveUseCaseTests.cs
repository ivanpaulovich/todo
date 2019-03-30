namespace TodoList.UnitTests
{
    using System.Linq;
    using System;
    using TodoList.Core.Boundaries.Remove;
    using TodoList.Core.Exceptions;
    using TodoList.Core.Gateways.InMemory;
    using TodoList.Core.Gateways;
    using TodoList.Core.UseCases;
    using Xunit;

    public sealed class RemoveUseCaseTests
    {
        string existingTodoItemId = "3b35f11e-7080-45e2-a152-afff5a325508";

        [Fact]
        public void RemoveChangesContext()
        {
            InMemoryContext inMemory = new InMemoryContext();
            IItemGateway gateway = new InMemoryItemGateway(inMemory);
            IUseCase sut = new Remove(gateway);

            sut.Execute(existingTodoItemId);

            Assert.Empty(inMemory.Items.Where(e => e.Id == new Guid(existingTodoItemId)));
        }

        [Fact]
        public void RemoveThrowsException_WhenItemDoesNotExist()
        {
            InMemoryContext inMemory = new InMemoryContext();
            IItemGateway gateway = new InMemoryItemGateway(inMemory);
            IUseCase sut = new Remove(gateway);

            Exception ex = Record.Exception(() => sut.Execute(Guid.NewGuid().ToString()));

            Assert.NotNull(ex);
            Assert.IsType<BusinessException>(ex);
        }
    }
}