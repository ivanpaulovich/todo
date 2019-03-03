namespace TodoList.Core {
    public interface IUseCase<in TInput> {
        void Execute (TInput input);
    }
}