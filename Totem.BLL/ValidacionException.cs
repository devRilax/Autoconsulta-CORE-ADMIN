using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Totem.BLL
{
    public class ValidacionException : Exception
    {
        public ValidacionException() { }
        public ValidacionException(string message) : base(message) { }
        public ValidacionException(string message, Exception inner) : base(message, inner) { }
    }
}
