namespace TodoList.IntegrationTests
{
    using Xunit;
    using TodoList.Infrastructure.EntityFrameworkGateway;
    using TodoList.Core.Entities;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    public class SqlTests
    {
        [Fact]
        public void SimpleUsage()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SqlContext>();
            optionsBuilder.UseSqlServer("Server=localhost;User Id=sa;Password=<YourNewStrong!Passw0rd>;Database=TodoItemsDB03");
            var context = new SqlContext(optionsBuilder.Options);
            var repository = new SqlItemGateway(context);

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