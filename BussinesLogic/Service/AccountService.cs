
using Entities.Entity;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using System.Security.Cryptography;
using BussinesLogic.Interface;
using System.Linq;

namespace BussinesLogic.Service
{
   public class AccountService : BaseService<Customer>,IAccount
    {
        
        public AccountService(IRepository<Customer> repository):base(repository)
        {
        
        }

       
        public Guid VerifyUser(string password, string userName)
        {
            var pass = Common.Encript.GetSHA1(password);
            var data=this._repository.FindByCondition(c => c.Password == pass && c.Email == userName).FirstOrDefault();
            if (data == null)
                throw new ArgumentException("Cliente no existe");
                     if (!data.IsActive)
                throw new ArgumentException("Cliente no esta activo");
           
            return data.Id;
        }
    }
}
