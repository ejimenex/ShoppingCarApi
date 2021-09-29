using ShoppingCar.Common.Token;
using AutoMapper;
using BussinesLogic.Interface;
using BussinesLogic.Profiles;
using BussinesLogic.Service;
using Entities.Entity;
using Microsoft.Extensions.DependencyInjection;

namespace BussinesLogic.Configuration
{
    public static partial class ServicesConfiguration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IBaseService<Customer>, CustomerService>();
            services.AddScoped<IBaseService<Product>, ProductService>();
            services.AddScoped<IBaseService<OrderDetail>, OrderDetailService>();
            services.AddScoped<IBaseService<OrderHeader>, OrderHeaderService>();
            services.AddScoped<IAccount, AccountService>();
            services.AddScoped<IOrderHeader, OrderHeaderService>();
            services.AddScoped<IOrderDetail, OrderDetailService>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CutomerProfile());
                mc.AddProfile(new ProductProfile());
                mc.AddProfile(new OrderDetailProfile());
                mc.AddProfile(new OrderHeaderProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc();
        }
    } 
    
}
