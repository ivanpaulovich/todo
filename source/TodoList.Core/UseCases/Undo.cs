namespace TodoList.Core.UseCases
{
    using TodoList.Core.Boundaries.Undo;
    using TodoList.Core.Entities;
    using TodoList.Core.Exceptions;
    using TodoList.Core.Gateways;

    public sealed class Undo : IUseCase
    {
        private IItemGateway _itemGateway;

        public Undo(IItemGateway itemGateway)
        {
            _itemGateway = itemGateway;
        }

        public void Execute(string itemId)
        {
            IItem item = _itemGateway.Get(itemId);

            if (item == null)
                throw new BusinessException($"Item with id { itemId } was not found.");

            item.Undo();
            _itemGateway.Update(item);
        }
    }
}