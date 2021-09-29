using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCar.Common.CustomException
{
    public class DuplicateValueException : Exception
    {
        public DuplicateValueException()
        {
        }

        public DuplicateValueException(string message)
            : base(message)
        {
        }

        public DuplicateValueException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
