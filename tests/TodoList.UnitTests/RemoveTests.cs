namespace TodoList.UnitTests
{
    using System;
    using System.Linq;
    using TodoList.Core.Boundaries.RemoveTodoItem;
    using TodoList.Core.Exceptions;
    using TodoList.Core.Gateways;
    using TodoList.Core.Gateways.InMemory;
    using TodoList.Core.UseCases;
    using Xunit;

    public sealed class RemoveTests
    {
        Guid existingTodoItemId = new Guid("3b35f11e-7080-45e2-a152-afff5a325508");

        [Fact]
        public void RemoveRemovesTodoItem()
        {
            InMemoryContext inMemory = new InMemoryContext();
            ITodoItemGateway gateway = new TodoItemGateway(inMemory);
            IUseCase sut = new RemoveTodoItem(gateway);

            sut.Execute(existingTodoItemId);

            Assert.Empty(inMemory.TodoItems.Where(e => e.Id == existingTodoItemId));
        }

        [Fact]
        public void RemoveThrowsException_WhenItemDoesNotExist()
        {
            InMemoryContext inMemory = new InMemoryContext();
            ITodoItemGateway gateway = new TodoItemGateway(inMemory);
            IUseCase sut = new RemoveTodoItem(gateway);

            Exception ex = Record.Exception(() => sut.Execute(Guid.NewGuid()));

            Assert.NotNull(ex);
            Assert.IsType<BusinessException>(ex);
        }
    }
}