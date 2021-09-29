using System;

namespace Repository.Dtos
{
    public class OrderHeaderDto:BaseDto
    {
        public Guid CustomerId { get; set; }
        public bool Invoiced { get; set; }
        public decimal? Total { get; set; }
        public decimal? TotalItems { get; set; }
        public CustomerDto Customer { get; set; }
    }
}
