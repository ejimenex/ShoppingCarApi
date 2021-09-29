using Entities.Entity;
using Microsoft.Extensions.DependencyInjection;
using Repository.Interface;
using Repository.Repo;

namespace Repository.Configuration
{
    public static partial class RepositoryConfiguration
    {
        public static void AddRespositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Customer>, CustormerRepository>();
            services.AddScoped <IRepository<Product>, IProductRepository>();
            services.AddScoped<IRepository<OrderDetail>, OrderDetailRepository>();
            services.AddScoped<IRepository<OrderHeader>, OrderHeaderRepository>();
          
        }
    }
}
