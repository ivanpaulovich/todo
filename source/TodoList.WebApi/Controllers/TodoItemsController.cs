using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoList.Core;
using TodoList.Core.Boundaries;
using TodoList.Core.Entities;
using TodoList.Core.Gateways;
using TodoList.Core.UseCases;
using TodoList.WebApi.Models;

namespace TodoList.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private IUseCase<Core.Boundaries.Todo.Request> _todoUseCase;
        private Core.Boundaries.Remove.IUseCase _removeUseCase;
        private Core.Boundaries.List.IUseCase _listUseCase;
        private IUseCase<Core.Boundaries.Rename.Request> _renameUseCase;
        private Core.Boundaries.Do.IUseCase _doUseCase;
        private Core.Boundaries.Undo.IUseCase _undoUseCase;
        private TodoPresenter _todoPresenter;
        private ListPresenter _listPresenter;

        public TodoItemsController(
            IUseCase<Core.Boundaries.Todo.Request> todoUseCase,
            Core.Boundaries.Remove.IUseCase removeUseCase,
            Core.Boundaries.List.IUseCase listUseCase,
            IUseCase<Core.Boundaries.Rename.Request> renameUseCase,
            Core.Boundaries.Do.IUseCase doUseCase,
            Core.Boundaries.Undo.IUseCase undoUseCase,
            TodoPresenter todoPresenter,
            ListPresenter listPresenter)
        {
            _todoUseCase = todoUseCase;
            _removeUseCase = removeUseCase;
            _listUseCase = listUseCase;
            _renameUseCase = renameUseCase;
            _doUseCase = doUseCase;
            _undoUseCase = undoUseCase;
            _todoPresenter = todoPresenter;
            _listPresenter = listPresenter;

        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<TodoItemViewModel>> Get()
        {
            _listUseCase.Execute();
            return _listPresenter.BuildResponse(this);
        }

        // POST api/values
        [HttpPost]
        public ActionResult<TodoItemViewModel> Post([FromBody] string value)
        {
            var request = new Core.Boundaries.Todo.Request(value);
            _todoUseCase.Execute(request);
            return _todoPresenter.BuildResponse(this);
        }

        [HttpPut("{id}")]
        public void Put(string id, [FromBody] string value)
        {
            var request = new Core.Boundaries.Rename.Request(id, value);
            _renameUseCase.Execute(request);
        }

        [HttpPut("{id}/Do")]
        public void Do(string id)
        {
            _doUseCase.Execute(id);
        }

        [HttpPut("{id}/Undo")]
        public void Undo(string id)
        {
            _undoUseCase.Execute(id);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _removeUseCase.Execute(id);
        }
    }
}