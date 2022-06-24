using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
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

        public CreateTodoItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {

            var entity = new TodoItem();

            //TODO:considering using mapper
            entity.Title = request.Title;
            entity.Note = request.Note;

            entity.ListId = request.ListId;
            entity.Created = DateTime.UtcNow;

            _context.TodoItems.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}