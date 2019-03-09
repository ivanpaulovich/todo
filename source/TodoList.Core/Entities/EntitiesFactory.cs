namespace TodoList.Core.Entities
{
    public sealed class EntitiesFactory : IEntitiesFactory
    {
        public ITodoItem NewTodoItem(string title)
        {
            var todoItem = new TodoItem();
            todoItem.UpdateTitle(title);
            return todoItem;
        }
    }
}