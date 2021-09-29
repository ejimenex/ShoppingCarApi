
using Entities.Entity;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ShoppingCar.Common.Constant;
using ShoppingCar.Common.CustomException;
using System.Linq;

namespace BussinesLogic.Service
{
   public class ProductService : BaseService<Product>
    {
        
        public ProductService(IRepository<Product> repository):base(repository)
        {
        
        }
        private void Validate(Product entity)
        {
            if (base.Exist(c => c.Name == entity.Name))
                throw new DuplicateValueException(Message.MessageDuplicateProduct);
        }
        public override Guid Create(Product entity)
        {
            this.Validate(entity);
            return base.Create(entity);
        }
    }
}
