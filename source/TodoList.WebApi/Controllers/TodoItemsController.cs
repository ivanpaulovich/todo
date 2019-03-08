using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoList.Core.UseCases;
using TodoList.Core.UseCases.AddTodoItem;
using TodoList.Core.UseCases.ListTodoItems;
using TodoList.Core.UseCases.UpdateTitle;
using TodoList.WebApi.Models;

namespace TodoList.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private IUseCase<AddTodoItemRequest> _addTodoItem;
        private IUseCase<Guid> _finishTodoItem;
        private IUseCase _listTodoItems;
        private IUseCase<UpdateTitleRequest> _updateTitle;
        private Presenter _presenter;

        public TodoItemsController(
            IUseCase<AddTodoItemRequest> addTodoItem,
            IUseCase<Guid> finishTodoItem,
            IUseCase listTodoItems,
            IUseCase<UpdateTitleRequest> updateTitle,
            Presenter presenter
        )
        {
            _addTodoItem = addTodoItem;
            _finishTodoItem = finishTodoItem;
            _listTodoItems = listTodoItems;
            _updateTitle = updateTitle;
            _presenter = presenter;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<TodoItemViewModel> Get()
        {
            _listTodoItems.Execute();
            return _presenter.ListItems;
        }

        // POST api/values
        [HttpPost]
        public ActionResult<TodoItemViewModel> Post([FromBody] string value)
        {
            AddTodoItemRequest request = new AddTodoItemRequest(value);
            _addTodoItem.Execute(request);
            return CreatedAtAction(nameof(Post), new { id = _presenter.CreatedItem.Id }, _presenter.CreatedItem);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] string value)
        {
            UpdateTitleRequest request = new UpdateTitleRequest(id, value);
            _updateTitle.Execute(request);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _finishTodoItem.Execute(id);
        }
    }
}