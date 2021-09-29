using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Interface
{
    public interface IOrderHeader
    {
        Guid GetCurrentOrder(Guid customer);
    }
}
