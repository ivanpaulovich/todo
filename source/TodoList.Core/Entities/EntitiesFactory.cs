namespace TodoList.Core.Entities
{
    public sealed class EntitiesFactory : IEntitiesFactory
    {
        public TodoItem NewTodoItem(string title)
        {
            var todoItem = new TodoItem(title);
            return todoItem;
        }
    }
}