using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<TodoListDBContext>(options => options.UseInMemoryDatabase("TodoListDb"));
            }
            else
            {
                var test=typeof(TodoListDBContext).Assembly;
                services.AddDbContext<TodoListDBContext>(options => options.UseNpgsql(configuration.GetConnectionString("TodoDB"),
                                                                                        b => b.MigrationsAssembly(typeof(TodoListDBContext).Assembly.FullName)));
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<TodoListDBContext>());

            // services.AddScoped<IDomainEventService, DomainEventService>();

            // services
            //     .AddDefaultIdentity<ApplicationUser>()
            //     .AddRoles<IdentityRole>()
            //     .AddEntityFrameworkStores<ApplicationDbContext>();

            // services.AddIdentityServer()
            //     .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            // services.AddTransient<IDateTime, DateTimeService>();
            // services.AddTransient<IIdentityService, IdentityService>();
            // services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

            // services.AddAuthentication()
            //     .AddIdentityServerJwt();

            // services.AddAuthorization(options =>
            // {
            //     options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator"));
            // });

            return services;
        }
    }
}