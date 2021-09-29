using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinesLogic.Interface
{
   public interface IAccount
    {
        Guid VerifyUser(string password, string userName);
        
    }
}
