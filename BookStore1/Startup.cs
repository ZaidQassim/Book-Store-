using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore1.Models.Repositories;
using BookStore1.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BookStore1
{
    public class Startup
    {

        private readonly IConfiguration configuration;
        public Startup(IConfiguration configuration)       
        {
            this.configuration = configuration;
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940 // تقم بتخزين السيزفيزز 
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();  

            //services.AddSingleton<IBookStorRepositore<Author>, AuthorRepository>();
            //services.AddSingleton<IBookStorRepositore<Book>, BookRepository>();
            services.AddScoped<IBookStorRepositore<Author>, AuthorDbRepository>();
            services.AddScoped<IBookStorRepositore<Book>, BookDbRepository>();


            services.AddDbContext<BookStoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("sqlCon"));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) // 
        {
            if (env.IsDevelopment())  
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            
            app.UseMvc(route =>
            {
                route.MapRoute("default", "{controller=Book}/{action=Index}/{id?}");
            });


        }

    }
}
