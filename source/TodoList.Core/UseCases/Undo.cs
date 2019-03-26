namespace TodoList.Core.UseCases
{
    using TodoList.Core.Boundaries.Undo;
    using TodoList.Core.Entities;
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
            item.Undo();
            _itemGateway.Update(item);
        }
    }
}