using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using TodoList.Core.Boundaries;
using TodoList.Core.Entities;
using TodoList.Core.Gateways;
using TodoList.Core.UseCases;
using TodoList.Infrastructure.InMemoryGateway;
using TodoList.WebApi.Controllers;

namespace TodoList.WebApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            AddSwagger(services);
            AddTodoListCore(services);
            AddInMemoryPersistence(services);
        }

        private void AddInMemoryPersistence(IServiceCollection services)
        {
            services.AddSingleton<InMemoryContext, InMemoryContext>();
            services.AddScoped<IItemGateway, InMemoryItemGateway>();
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API (Development)", Version = "v1" });
            });
        }

        private void AddTodoListCore(IServiceCollection services)
        {
            services.AddScoped<IEntitiesFactory, EntitiesFactory>();

            services.AddScoped<TodoPresenter, TodoPresenter>();
            services.AddScoped<ListPresenter, ListPresenter>();

            services.AddScoped<IResponseHandler<Core.Boundaries.Todo.Response>>(x => x.GetRequiredService<TodoPresenter>());
            services.AddScoped<IResponseHandler<Core.Boundaries.List.Response>>(x => x.GetRequiredService<ListPresenter>());

            services.AddScoped<IUseCase<Core.Boundaries.Todo.Request>, Todo>();
            services.AddScoped<Core.Boundaries.Remove.IUseCase, Remove>();
            services.AddScoped<Core.Boundaries.List.IUseCase, List>();
            services.AddScoped<IUseCase<Core.Boundaries.Rename.Request>, Rename>();
            services.AddScoped<Core.Boundaries.Do.IUseCase, Do>();
            services.AddScoped<Core.Boundaries.Undo.IUseCase, Undo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            UseSwagger(app);

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void UseSwagger(IApplicationBuilder app)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}