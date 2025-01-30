using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Petshop.Domain.Validations
{
    public class DomainExceptionValidations : Exception
    {
        public DomainExceptionValidations(string error) : base(error){}
        public static void When(bool hasError, string error) 
        {
            if (hasError)
            {
                throw new DomainExceptionValidations(error);
            }
        }
    }
}