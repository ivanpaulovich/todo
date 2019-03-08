namespace TodoList.Core.Boundaries
{
    public interface IResponseHandler<in TResponse>
    {
        void Handle(TResponse response);
    }
}