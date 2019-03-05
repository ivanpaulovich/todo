namespace TodoList.Core.Entities
{
    public interface IEntitiesFactory
    {
        TodoItem NewTodoItem(string title);
    }
}