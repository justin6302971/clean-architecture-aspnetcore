using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.TodoItems.Commands;
using CleanArchitecture.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using CleanArchitecture.Application.TodoLists.Queries.GetTodos;
using CleanArchitecture.WebApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchitecture.WebUI.Controllers
{
    // [Authorize]
    public class TodoItemsController : ApiController
    {
        private readonly IMediator _mediator;

        public TodoItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<TodoItemDto>>> GetTodoItemsWithPagination([FromQuery] GetTodoItemsWithPaginationQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateTodoItemCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}

