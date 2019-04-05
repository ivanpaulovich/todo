namespace TodoList.IntegrationTests
{
    using Xunit;
    using TodoList.Infrastructure.FileSystemGateway;
    using TodoList.Core.Entities;
    using System.Linq;

    public sealed class FileSystemTests
    {
        [Fact]
        public void SimpleUsage()
        {
            var repository = new FileSystemItemGateway();

            var item1 = new Item();
            item1.Rename("My test task");
            repository.Add(item1);
            var items = repository.List();
            repository.Delete(items.FirstOrDefault().Id.ToString());
            var item2 = new Item();
            item2.Rename("Initial name");
            repository.Add(item2);
            var getItem = repository.Get(item2.Id.ToString());
            getItem.Rename("Update Title");
            repository.Update(getItem);
            var getItem2 = repository.Get(item2.Id.ToString());
            Assert.Equal("Update Title", getItem2.Title);
        }
    }
}