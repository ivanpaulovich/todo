namespace TodoList.Core.Entities
{
    public sealed class EntitiesFactory : IEntitiesFactory
    {
        public TodoItem NewTodoItem(string title)
        {
            return new TodoItem(title);
        }
    }
}