using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.TodoItems.Commands;
using CleanArchitecture.Application.TodoItems.Notifications;
using CleanArchitecture.Application.TodoItems.Queries.GetTodoItemQuery;
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
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class TodoItemsController : ApiController
    {
        private readonly IMediator _mediator;


        private readonly ISender _sender;

        private readonly IPublisher _publisher;



        public TodoItemsController(IMediator mediator, ISender sender, IPublisher publisher)
        {
            _mediator = mediator;
            _sender = sender;
            _publisher = publisher;

        }



        [HttpGet]
        public async Task<ActionResult<PaginatedList<TodoItemDto>>> GetTodoItemsWithPagination([FromQuery] GetTodoItemsWithPaginationQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDto>> GetTodoItem([FromRoute] GetTodoItemQuery query)
        {
            return await _sender.Send(query);
        }



        [HttpPost]
        public async Task<ActionResult> Create(CreateTodoItemCommand command)
        {
            var createdTodoListId = await _mediator.Send(command);
            // return await _mediator.Send(command);



            return CreatedAtAction(nameof(GetTodoItem), new { id = createdTodoListId }, createdTodoListId);
        }
    }
}

