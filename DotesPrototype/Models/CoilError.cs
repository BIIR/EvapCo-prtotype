using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotesPrototype.Models
{
    public class CoilError
    {
        public bool HasError { get; set; }
        public string Message { get; set; }
        public CoilError()
        {
            HasError = false;
            Message = "";
        }
    }
}
