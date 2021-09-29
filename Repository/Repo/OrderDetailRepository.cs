using Entities;
using Entities.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Repository.Repo
{
    public class OrderDetailRepository : RepositoryBase<OrderDetail>
    {
        public OrderDetailRepository(ShoppingContext ctx) : base(ctx)
        {

        }
        public override IQueryable<OrderDetail> FindAll()
        {
            return base.FindAll().Include(c=> c.Product);
        }
    }
}
