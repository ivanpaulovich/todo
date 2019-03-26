namespace TodoList.Core.Boundaries.Undo
{
    public interface IUseCase
    {
        void Execute(string itemId);
    }
}