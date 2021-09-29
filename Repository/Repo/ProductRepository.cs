using Entities;
using Entities.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Repo
{
    public class IProductRepository:RepositoryBase<Product>
    {
        public IProductRepository(ShoppingContext ctx):base(ctx)
        {

        }
    }
}
