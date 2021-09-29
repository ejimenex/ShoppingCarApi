using Entities;
using Entities.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Repository.Repo
{
    public class OrderHeaderRepository : RepositoryBase<OrderHeader>
    {
        public OrderHeaderRepository(ShoppingContext ctx) : base(ctx)
        {

        }
        public override IQueryable<OrderHeader> FindAll()
        {
            return base.FindAll().Include(v=> v.Customer);
        }
    }
}
