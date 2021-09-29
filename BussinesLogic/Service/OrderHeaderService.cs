
using BussinesLogic.Interface;
using Entities.Entity;
using Repository.Interface;
using System;
using System.Linq;

namespace BussinesLogic.Service
{
    public class OrderHeaderService : BaseService<OrderHeader>, IOrderHeader
    {
        
        public OrderHeaderService(IRepository<OrderHeader> repository) :base(repository)
        {
           
        }
        public override Guid Create(OrderHeader entity)
        {
            entity.Customer = null;
            entity.Invoiced = false;
            return base.Create(entity);
        }
        public Guid GetCurrentOrder(Guid customer)
        {
            var result = base.FindByCondition(c => c.CustomerId == customer && !c.Invoiced).FirstOrDefault();
            if (result is null)
                return base.Create(new OrderHeader { Invoiced = false, CustomerId = customer, Total = 0, TotalItems = 0, Customer = null });
            return result.Id;

        }   
    }
}
