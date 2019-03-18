namespace TodoList.UnitTests
{
    using System;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways.InMemory;
    using TodoList.Core.Gateways;
    using TodoList.Core.UseCases;
    using TodoList.Core;
    using TodoList.Core.Boundaries.MarkItemCompleted;
    using Xunit;
    using System.Linq;

    public sealed class MarkItemIncompletedTests
    {
        Guid existingTodoItemId = new Guid("3b35f11e-7080-45e2-a152-afff5a325508");

        [Fact]
        public void MarkItemIncompletedSuccess()
        {
            InMemoryContext inMemory = new InMemoryContext();
            ITodoItemGateway gateway = new TodoItemGateway(inMemory);
            IUseCase sut = new MarkItemIncompleted(gateway);

            sut.Execute(existingTodoItemId);

            Assert.NotEmpty(inMemory.TodoItems.Where(e => e.Id == existingTodoItemId && !e.IsCompleted));
        }

        [Fact]
        public void MarkItemCompletedDoesNotChange()
        {
            InMemoryContext inMemory = new InMemoryContext();
            ITodoItemGateway gateway = new TodoItemGateway(inMemory);
            IUseCase sut = new MarkItemCompleted(gateway);

            sut.Execute(Guid.NewGuid());

            Assert.NotEmpty(inMemory.TodoItems.Where(e => e.Id == existingTodoItemId && !e.IsCompleted));
        }
    }
}