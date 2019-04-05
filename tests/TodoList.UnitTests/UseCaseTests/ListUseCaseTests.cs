namespace TodoList.UseCaseTests.UnitTests
{
    using TodoList.Core.UseCases;
    using TodoList.Infrastructure.InMemoryGateway;
    using Xunit;

    public sealed class ListUseCaseTests
    {
        [Fact]
        public void GivenContext_ReturnsItems()
        {
            var context = new InMemoryContext();
            var gateway = new InMemoryItemGateway(context);
            var responseHandler = new ResponseHandler();
            var list = new List(responseHandler, gateway);
            list.Execute();
            Assert.NotNull(responseHandler.ListItems);
        }
    }
}