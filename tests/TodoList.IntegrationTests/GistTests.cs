namespace TodoList.IntegrationTests
{
    using System.Linq;
    using TodoList.Core.Entities;
    using TodoList.Infrastructure.GistGateway;
    using Xunit;

    // Generate a token on GitHub at https://github.com/settings/tokens
    // then edit the `appsettings.json`

    public sealed class GistTests
    {
        [Fact(Skip = "Requires a GitHub account and tokens to run")]
        public void BasicTests()
        {
            var gateway = new GistItemGateway();
            var item1 = new Item();
            item1.Rename("My test task");
            gateway.Add(item1);
            var items = gateway.List();
            gateway.Delete(items.FirstOrDefault().Id.ToString());
            var item2 = new Item();
            item2.Rename("Initial name");
            gateway.Add(item2);
            var getItem = gateway.Get(item2.Id.ToString());
            getItem.Rename("Update Title");
            gateway.Update(getItem);
            var getItem2 = gateway.Get(item2.Id.ToString());
            Assert.Equal("Update Title", getItem2.Title);
        }
    }
}