namespace TodoList.Core.Boundaries.Remove
{
    public interface IUseCase
    {
        void Execute(string itemId);
    }
}