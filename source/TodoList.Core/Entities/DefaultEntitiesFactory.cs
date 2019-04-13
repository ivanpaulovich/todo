namespace TodoList.Core.Entities
{
    public sealed class DefaultEntitiesFactory : IEntitiesFactory
    {
        public IItem NewTodo() 
        {
            var todo = new Item();
            return todo;
        }
    }
}