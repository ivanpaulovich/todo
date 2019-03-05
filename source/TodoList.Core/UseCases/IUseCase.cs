namespace TodoList.Core.UseCases
{
    public interface IUseCase<in TInput>
    {
        void Execute(TInput input);
    }
}