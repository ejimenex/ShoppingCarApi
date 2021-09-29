using Entities;
using Entities.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Repo
{
   public class CustormerRepository:RepositoryBase<Customer>
    {
        public CustormerRepository(ShoppingContext ctx) : base(ctx) {

        }

    }
}
