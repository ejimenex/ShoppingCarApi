using System;

namespace Entities.Entity
{
    public class OrderHeader:BaseClass
    {
        public Guid CustomerId { get; set; }
        public bool Invoiced { get; set; }
        public decimal? Total { get; set; }
        public decimal? TotalItems { get; set; }
        public Customer Customer { get; set; }
    }
}
