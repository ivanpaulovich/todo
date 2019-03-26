namespace TodoList.Core.Entities
{
    public interface IEntitiesFactory
    {
        IItem NewTodo();
    }
}