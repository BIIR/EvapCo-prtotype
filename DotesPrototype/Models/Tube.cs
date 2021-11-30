using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotesPrototype.Models
{
    public class Tube
    {
        public int Row { get;  }
        public int Column { get;  }
        public Tube(int row,int column)
        {
            Row = row;
            Column = column;
        }
    }
}
