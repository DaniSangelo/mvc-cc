using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IES.Models.Exceptions
{
    public class NoInstitutionException : ApplicationException
    {
        public NoInstitutionException(string message) : base(message) { }
    }
}
