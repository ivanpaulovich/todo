namespace TodoList.Core.UseCases {
    public interface IUseCaseOutputHandler<in TOutput> {
        void Handle (TOutput output);
    }
}