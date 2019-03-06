namespace TodoList.UnitTests
{
    using System;
    using TodoList.Core.Gateways.InMemory;
    using TodoList.Core.UseCases.ListTodoItems;
    using TodoList.Core.UseCases;
    using Xunit;

    public sealed class ListTodoItemsTests
    {
        [Fact]
        public void GivenNullContext_ThrowsException()
        {
            var context = new DBContext();
            var gateway = new TodoItemGateway(context);
            var outputHandler = new OutputHandler();
            var list = new Interactor(outputHandler, gateway);
            list.Execute();
        }
    }
}