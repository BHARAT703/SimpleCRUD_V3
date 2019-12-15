using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SimpleCRUD.Infrastructure.Repositories;
using SimpleCRUD.Infrastructure.Services;
using SimpleCRUD.Infrastructure.Uow;

namespace SimpleCRUD
{
    public class Startup
    {
        private string connectionString = string.Empty;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            Console.WriteLine("ConfigureServices : " + connectionString);

            //Bharat : register application db context
            services.AddDbContext<Infrastructure.DatabaseContext.ApplicationContext>(options => options.UseSqlServer(connectionString: connectionString, m => m.MigrationsAssembly("SimpleCRUD")));

            //added dependency injection instances
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //TODO: typeof not working need to inject manually for now.
            //services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            //services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            //services.AddScoped(typeof(IBaseServiceWithFullAudit<>), typeof(BaseServiceWithFullAudit<>));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            // Automapper Configuration
            services.AddAutoMapper(typeof(Startup));

            //Enable Cors
            services.AddCors();

            services.AddMvc();

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v3" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();

            // Add MVC to the request pipeline.
            app.UseCors(builder =>
                builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SimpleCRUD V3");
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
