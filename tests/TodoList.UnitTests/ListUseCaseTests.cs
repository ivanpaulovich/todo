namespace TodoList.UnitTests
{
    using System;
    using TodoList.Core.Gateways.InMemory;
    using TodoList.Core.UseCases;
    using Xunit;

    public sealed class ListUseCaseTests
    {
        [Fact]
        public void GivenContext_ReturnsItems()
        {
            var context = new InMemoryContext();
            var gateway = new ItemGateway(context);
            var responseHandler = new ResponseHandler();
            var list = new List(responseHandler, gateway);
            list.Execute();
            Assert.NotNull(responseHandler.ListItems);
        }
    }
}