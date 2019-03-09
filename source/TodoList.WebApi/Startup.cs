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
using TodoList.Core.Boundaries.AddTodoItem;
using TodoList.Core.Boundaries.ListTodoItems;
using TodoList.Core.Boundaries.UpdateTitle;
using TodoList.Core.Entities;
using TodoList.Core.Gateways;
using TodoList.Core.UseCases;
using TodoList.Infrastructure.EntityFrameworkDataAccess;
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
            AddSQL(services);
            AddTodoListCore(services);
        }

        private void AddSQL(IServiceCollection services)
        {
            services.AddDbContext<TodoListContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
        }

        private void AddTodoListCore(IServiceCollection services)
        {
            services.AddScoped<ITodoItemGateway, TodoItemGateway>();
            services.AddScoped<IEntitiesFactory, EntitiesFactory>();

            services.AddScoped<Presenter, Presenter>();
            services.AddScoped<IResponseHandler<Core.Boundaries.AddTodoItem.Response>>(x => x.GetRequiredService<Presenter>());
            services.AddScoped<IResponseHandler<Core.Boundaries.ListTodoItems.Response>>(x => x.GetRequiredService<Presenter>());

            services.AddScoped<IUseCase<Core.Boundaries.AddTodoItem.Request>, AddTodoItem>();
            services.AddScoped<Core.Boundaries.FinishTodoItem.IUseCase, Core.UseCases.FinishTodoItem>();
            services.AddScoped<Core.Boundaries.ListTodoItems.IUseCase, Core.UseCases.ListTodoItems>();
            services.AddScoped<IUseCase<Core.Boundaries.UpdateTitle.Request>, Core.UseCases.UpdateTitle>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

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
            });
        }
    }
}