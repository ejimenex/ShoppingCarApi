using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Dtos
{
   public class BaseDto
    {
        public Guid Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
