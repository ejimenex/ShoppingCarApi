
using Entities.Entity;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using BussinesLogic.Common;
using ShoppingCar.Common.Constant;
using ShoppingCar.Common.CustomException;

namespace BussinesLogic.Service
{
   public class CustomerService:BaseService<Customer>
    {
        public CustomerService(IRepository<Customer> repository):base(repository)
        {
        }
        private void Validate(Customer entity) {

            if (base.Exist(c => c.DocumentNumber == entity.DocumentNumber))
                throw new DuplicateValueException(Message.MessageDuplicateCustomerDocument);
            if (base.Exist(c => c.Email == entity.Email))
                throw new DuplicateValueException(Message.MessageDuplicateEmail);
        }
        public override Guid Create(Customer entity)
        {
            this.Validate(entity);
            entity.Password = Encript.GetSHA1(entity.Password);
            return base.Create(entity);               
        }
    }
}
