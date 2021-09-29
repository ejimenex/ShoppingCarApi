using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Interface
{
   public interface IOrderDetail
    {
        IEnumerable<OrderDetail> GetByOrder(Guid Order);
    }
}
