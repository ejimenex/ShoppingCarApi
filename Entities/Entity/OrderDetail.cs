using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entity
{
  public class OrderDetail:BaseClass
    {
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal Total { get; set; }
        public virtual Product Product { get; set; }
    }
}
