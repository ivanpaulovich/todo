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
                    new TodoItem() { Id = new Guid("3b35f11e-7080-45e2-a152-afff5a325508"), Title = "Create Repository" },
                    new TodoItem() { Id = new Guid("4b2f8170-c618-4cd6-91b9-25e3b2bfa53e"), Title = "Create solution" },
                    new TodoItem() { Id = new Guid("360644f3-abb5-410b-939d-78a6d07bd075"), Title = "Add projects" },
                    new TodoItem() { Id = new Guid("f1f0adf8-255f-45ef-9528-d6c2c326240b"), Title = "Commit code" },
                    new TodoItem() { Id = new Guid("72af359b-48d7-41cd-978b-38c82e1206d4"), Title = "Push" }
                );
        }
    }
}