using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events;
using MediatR;

namespace CleanArchitecture.Application.TodoItems.Commands
{
    public class CreateTodoItemCommand : IRequest<int>
    {
        public int ListId { get; set; }
        public string Title { get; set; }

        public string Note { get; set; }

    }


    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, int>
    {
        private readonly IApplicationDbContext _context;

        private readonly IPublisher _publisher;


        public CreateTodoItemCommandHandler(IApplicationDbContext context, IPublisher publisher)
        {
            _context = context;
            _publisher = publisher;
        }

        public async Task<int> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {

            var entity = new TodoItem();

            //TODO:considering using mapper
            entity.Title = request.Title;
            entity.Note = request.Note;

            entity.ListId = request.ListId;
            entity.Created = DateTime.UtcNow;

            entity.DomainEvents.Add(new TodoItemCreatedEvent(entity));

            _context.TodoItems.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);
            // var createEvent = new TodoItemCreatedEvent(entity);
            // var domainEvent = new DomainEventNotification<TodoItemCreatedEvent>(createEvent);
            // await _publisher.Publish(domainEvent);

            return entity.Id;
        }
    }
}