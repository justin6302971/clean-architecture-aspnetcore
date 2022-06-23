using CleanArchitecture.Application.TodoLists.Commands.CreateTodoList;
using CleanArchitecture.Application.TodoLists.Queries.GetTodos;
using CleanArchitecture.WebApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.WebUI.Controllers
{
    // [Authorize]
    public class TodoListsController : ApiController
    {
        private readonly IMediator _mediator;

        public TodoListsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<TodoListDto>>> Get()
        {
            return await _mediator.Send(new GetTodosQuery());
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateTodoListCommand command)
        {
            return await _mediator.Send(command);
        }


        // [HttpGet("{id}")]
        // public async Task<FileResult> Get(int id)
        // {
        //     var vm = await Mediator.Send(new ExportTodosQuery { ListId = id });

        //     return File(vm.Content, vm.ContentType, vm.FileName);
        // }

        // [HttpPost]
        // public async Task<ActionResult<int>> Create(CreateTodoListCommand command)
        // {
        //     return await Mediator.Send(command);
        // }

        // [HttpPut("{id}")]
        // public async Task<ActionResult> Update(int id, UpdateTodoListCommand command)
        // {
        //     if (id != command.Id)
        //     {
        //         return BadRequest();
        //     }

        //     await Mediator.Send(command);

        //     return NoContent();
        // }

        // [HttpDelete("{id}")]
        // public async Task<ActionResult> Delete(int id)
        // {
        //     await Mediator.Send(new DeleteTodoListCommand { Id = id });

        //     return NoContent();
        // }
    }
}