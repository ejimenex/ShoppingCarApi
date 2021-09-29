
using BussinesLogic.Interface;
using Entities.Entity;
using Repository.Interface;
using Repository.Repo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BussinesLogic.Service
{
    public class OrderDetailService:BaseService<OrderDetail>, IOrderDetail
    {
        private readonly IRepository<Product> productRepository;
        private readonly IRepository<OrderHeader> orderRepository;
        public OrderDetailService(IRepository<OrderDetail> repository, IRepository<OrderHeader> orderRepository, IRepository<Product> productRepository) :base(repository)
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
        }
        private void UpdateHeaderAndStock(OrderDetail entity, bool IsDelete=false) {
            var product = this.productRepository.GetOne(entity.ProductId);
            if (IsDelete)
            {
                product.Stock += entity.Quantity;
            }
            else { product.Stock -= entity.Quantity; }
            
            productRepository.Update(product);
            var order = orderRepository.GetOne(entity.OrderId);
            if (IsDelete)
            {
                order.TotalItems--;
                order.Total -= entity.Total;
            }
            else {
                order.TotalItems++;
                order.Total += entity.Total;
            }
            
            orderRepository.Update(order);
        }
        public override Guid Create(OrderDetail entity)
        {
            UpdateHeaderAndStock(entity);
            return base.Create(entity);
        }
        public override void Delete(Guid Id)
        {
            var detail = base.GetOne(Id);
            UpdateHeaderAndStock(detail,true);
            base.Delete(Id);
        }

        public IEnumerable<OrderDetail> GetByOrder(Guid Order) {
            return base.FindAll().Where(c => c.OrderId == Order);
        }
    }
}
