namespace TodoList.Core.UseCases
{
    public interface IOutputHandler<in TOutput>
    {
        void Handle(TOutput output);
    }
}