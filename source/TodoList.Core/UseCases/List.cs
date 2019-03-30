namespace TodoList.Core.UseCases
{
    using System.Collections.Generic;
    using TodoList.Core.Boundaries.List;
    using TodoList.Core.Boundaries;
    using TodoList.Core.Entities;
    using TodoList.Core.Gateways;

    public sealed class List : IUseCase
    {
        private IResponseHandler<Response> _responseHandler;
        private IItemGateway _itemGateway;

        public List(
            IResponseHandler<Response> responseHandler,
            IItemGateway itemGateway)
        {
            _responseHandler = responseHandler;
            _itemGateway = itemGateway;
        }

        public void Execute()
        {
            var items = _itemGateway.List();
            Response response = BuildResponse(items);
            _responseHandler.Handle(response);
        }

        private Response BuildResponse(IList<IItem> items)
        {
            ResponseBuilder builder = new ResponseBuilder();
            foreach (var item in items)
            {
                if (item.Done)
                    builder.WithDone(item.Id, item.Title);
                else
                    builder.WithUndone(item.Id, item.Title);
            }

            return builder.Build();
        }
    }
}