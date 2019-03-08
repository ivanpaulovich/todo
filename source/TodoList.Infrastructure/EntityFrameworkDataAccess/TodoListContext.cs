namespace TodoList.Infrastructure.EntityFrameworkDataAccess
{
    using Microsoft.EntityFrameworkCore;
    using TodoList.Core.Entities;

    public sealed class TodoListContext : DbContext
    {
        public TodoListContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>()
                .ToTable("TodoItem")
                .HasKey(p => p.Id);
        }
    }
}