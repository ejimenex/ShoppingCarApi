using AutoMapper;
using BussinesLogic.Configuration;
using Entities;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Repository.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace ApiMedical
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

            
            services.AddODataQueryFilter();

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;

            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews();
            //services.AddOData();
        
            services.AddSwaggerGen(c =>
            {     // Set the comments path for the Swagger JSON and UI.
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Medical", Version = "v1" });
            });
            //services.Configure<MvcOptions>(options =>
            //{
            //    options.Filters.Add(new CorsAuthorizationFilterFactory("EveryOne"));
            //});
            services.AddServices();
            services.AddRespositories();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            var connectionString = Configuration["sqlconnection:connectionString"];
            services.AddDbContext<ShoppingContext>(options =>

                  options.UseSqlServer(connectionString).UseLazyLoadingProxies());

            var mapper = Maping.Mapping.GetMapper();
            services.AddSingleton(mapper);
          // Infrastructure.Interface.Infrastructure.LoadConfiguration(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseCors("CorsPolicy");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            //app.UseRouting();
            //app.UseEndpoint(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
            app.UseMvc(routeBuilder =>
            {
                //routeBuilder.EnableDependencyInjection();
                
                //routeBuilder.Select().Filter().OrderBy().Expand().Count().MaxTop(10);
                //routeBuilder.MapODataServiceRoute("odata", "api", GetEdmModel());
            });
        }
      

    }
}
