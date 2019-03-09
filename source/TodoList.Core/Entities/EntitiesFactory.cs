namespace TodoList.Core.Entities
{
    public sealed class EntitiesFactory : IEntitiesFactory
    {
        public ITodoItem NewTodoItem()
        {
            var todoItem = new TodoItem();
            return todoItem;
        }
    }
}