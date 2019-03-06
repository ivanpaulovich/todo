using System;

namespace TodoList.WebApi.Models
{
    public sealed class TodoItemViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
}