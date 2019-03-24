namespace TodoList.UnitTests
{
    using System;
    using TodoList.Core.Gateways.InMemory;
    using TodoList.Core.UseCases;
    using Xunit;

    public sealed class ListTodoItemsTests
    {
        [Fact]
        public void GivenContext_ReturnsItems()
        {
            var context = new InMemoryContext();
            var gateway = new TodoItemGateway(context);
            var responseHandler = new ResponseHandler();
            var list = new ListTodoItems(responseHandler, gateway);
            list.Execute();
            Assert.NotNull(responseHandler.ListTodoItems);
        }
    }
}