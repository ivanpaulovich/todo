namespace TodoList.Infrastructure.EntityFrameworkDataAccess
{
    using System;
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

            modelBuilder.Entity<TodoItem>()
                .HasData(
                    TodoItem.Restore(new Guid("3b35f11e-7080-45e2-a152-afff5a325508"), "Create Repository"),
                    TodoItem.Restore(new Guid("4b2f8170-c618-4cd6-91b9-25e3b2bfa53e"), "Create solution"),
                    TodoItem.Restore(new Guid("360644f3-abb5-410b-939d-78a6d07bd075"), "Add projects"),
                    TodoItem.Restore(new Guid("f1f0adf8-255f-45ef-9528-d6c2c326240b"), "Commit code"),
                    TodoItem.Restore(new Guid("72af359b-48d7-41cd-978b-38c82e1206d4"), "Push")
                );
        }
    }
}