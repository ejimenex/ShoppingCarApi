using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Dtos
{
    public class OrderDetailDto : BaseDto
    {
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal Total { get; set; }
        public ProductDto Product { get; set; }
    }
}
