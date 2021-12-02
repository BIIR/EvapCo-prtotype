using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DotesPrototype.Models
{
    public class Circuit
    {
        public List<Tube> Tubes { get; set; }

        public Circuit()
        {
            Tubes = new List<Tube>();
        }
        public void Add(Tube tube)
        {
            Tubes.Add(tube);
        }

        internal PointCollection getPoints(int xstep, int ystep, int offset, bool offsetBool)
        {
            PointCollection result = new PointCollection();
            for (int i = 0; i < Tubes.Count; i++)
            {
                if (offsetBool && i % 2 != 0)
                {
                    result.Add(new System.Windows.Point(offset + xstep * Tubes[i].Column+7.5, ystep * Tubes[i].Row + 7.5));
                }
                else
                {
                    result.Add(new System.Windows.Point(xstep * Tubes[i].Column + 7.5, ystep * Tubes[i].Row + 7.5));
                }
            }
            return result;
        }
    }
}
