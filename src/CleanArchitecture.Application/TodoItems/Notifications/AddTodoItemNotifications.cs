using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.TodoItems.Notifications
{
    public class AddTodoItemNotifications : INotification
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class EmailHandler : INotificationHandler<AddTodoItemNotifications>
    {
        private readonly ILogger<EmailHandler> _logger;
        public EmailHandler(ILogger<EmailHandler> logger)
        {
            _logger = logger;

        }
        public Task Handle(AddTodoItemNotifications notification, CancellationToken cancellationToken)
        {
            // Trace.WriteLine($"send email,Id:{notification.Id},Name:{notification.Name}");
            _logger.LogInformation($"send email,Id:{notification.Id},Name:{notification.Name}");


            return Task.CompletedTask;
        }
    }

    public class CacheHandler : INotificationHandler<AddTodoItemNotifications>
    {
        private readonly ILogger<CacheHandler> _logger;
        public CacheHandler(ILogger<CacheHandler>  logger)
        {
            _logger = logger;

        }

        public Task Handle(AddTodoItemNotifications notification, CancellationToken cancellationToken)
        {
            // Trace.WriteLine($"setup cache,Id:{notification.Id},Name:{notification.Name}");
            _logger.LogInformation($"setup cache,Id:{notification.Id},Name:{notification.Name}");

            return Task.CompletedTask;
        }
    }
}