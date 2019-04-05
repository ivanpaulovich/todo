namespace TodoList.UseCaseTests.UnitTests
{
    using System;
    using System.Linq;
    using TodoList.Core.Boundaries.Remove;
    using TodoList.Core.Gateways;
    using TodoList.Core.UseCases;
    using TodoList.Infrastructure.InMemoryGateway;
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
    }
}