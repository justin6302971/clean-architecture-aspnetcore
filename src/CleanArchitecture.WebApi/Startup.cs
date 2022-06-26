using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.WebApi.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(Configuration);

            services.AddHttpContextAccessor();

            services.AddControllers();

            #region swagger settings
            services.AddApiVersioning(setup =>
                       {
                           setup.DefaultApiVersion = new ApiVersion(1, 0);
                           setup.AssumeDefaultVersionWhenUnspecified = true;
                           setup.ReportApiVersions = true;
                       });

            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen();

            services.ConfigureOptions<ConfigureSwaggerOptions>();
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(options =>
              {
                  foreach (var description in provider.ApiVersionDescriptions)
                  {
                      string docName = $"Todo.Api {description.GroupName.ToUpperInvariant()}";
                      options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", docName);
                  }
              });


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
