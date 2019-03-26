namespace TodoList.Core.Boundaries.Do
{
    public interface IUseCase
    {
        void Execute(string itemId);
    }
}