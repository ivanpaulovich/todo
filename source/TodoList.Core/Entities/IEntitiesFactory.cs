namespace TodoList.Core.Entities
{
    public interface IEntitiesFactory
    {
        ITodoItem NewTodoItem(string title);
    }
}