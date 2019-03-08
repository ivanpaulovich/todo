using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using TodoList.Core.Entities;
using TodoList.Core.Gateways;
using TodoList.Core.Gateways.InMemory;
using TodoList.Core.UseCases;
using TodoList.Core.UseCases.AddTodoItem;
using TodoList.Core.UseCases.ListTodoItems;
using TodoList.Core.UseCases.UpdateTitle;
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            services.AddSingleton<DBContext, DBContext>();
            services.AddScoped<ITodoItemGateway, TodoItemGateway>();
            services.AddScoped<IEntitiesFactory, EntitiesFactory>();

            services.AddScoped<Presenter, Presenter>();
            services.AddScoped<IOutputHandler<AddTodoItemResponse>>(x => x.GetRequiredService<Presenter>());
            services.AddScoped<IOutputHandler<ListTodoItemsResponse>>(x => x.GetRequiredService<Presenter>());

            services.AddScoped<IUseCase<AddTodoItemRequest>, Core.UseCases.AddTodoItem.Interactor>();
            services.AddScoped<IUseCase<Guid>, Core.UseCases.FinishTodoItem.Interactor>();
            services.AddScoped<IUseCase, Core.UseCases.ListTodoItems.Interactor>();
            services.AddScoped<IUseCase<UpdateTitleRequest>, Core.UseCases.UpdateTitle.Interactor>();
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

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}