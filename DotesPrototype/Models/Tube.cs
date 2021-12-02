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

        public override bool Equals(object obj)
        {
            return obj is Tube tube &&
                   Row == tube.Row &&
                   Column == tube.Column;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }
    }
}
