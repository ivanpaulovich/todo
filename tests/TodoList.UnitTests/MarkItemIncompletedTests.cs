namespace TodoList.UnitTests
{
    using System.Linq;
    using System;
    using TodoList.Core.Boundaries.MarkItemIncomplete;
    using TodoList.Core.Entities;
    using TodoList.Core.Exceptions;
    using TodoList.Core.Gateways.InMemory;
    using TodoList.Core.Gateways;
    using TodoList.Core;
    using Xunit;
    using TodoList.Core.UseCases;

    public sealed class MarkItemIncompletedTests
    {
        Guid existingTodoItemId = new Guid("3b35f11e-7080-45e2-a152-afff5a325508");

        [Fact]
        public void MarkItemIncompletedSuccess()
        {
            InMemoryContext inMemory = new InMemoryContext();
            ITodoItemGateway gateway = new TodoItemGateway(inMemory);
            IUseCase sut = new MarkItemIncomplete(gateway);

            sut.Execute(existingTodoItemId);

            Assert.NotEmpty(inMemory.TodoItems.Where(e => e.Id == existingTodoItemId && !e.IsCompleted));
        }

        [Fact]
        public void MarkItemCompleted_ThrowsException_WhenItemDoesNotExist()
        {
            InMemoryContext inMemory = new InMemoryContext();
            ITodoItemGateway gateway = new TodoItemGateway(inMemory);
            IUseCase sut = new MarkItemIncomplete(gateway);

            Exception ex = Record.Exception(() => sut.Execute(Guid.NewGuid()));

            Assert.NotNull(ex);
            Assert.IsType<BusinessException>(ex);
        }
    }
}