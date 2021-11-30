using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotesPrototype.Models
{
    public class Coil
    {
        public List<List<Tube>> TubeRows { get; set; }
        public List<Circuit> Circuits { get; set; }
        public bool Offset { get; set; }
        public int RowsDeep { get; set; }
        public int RowCount { get; set; }
        public int TubesCount { get
            {
                int count = 0;
                foreach(var tubeRow in TubeRows)
                {
                    count += tubeRow.Count;
                }
                return count;
            }
        }

        public Coil(bool offset, int rowsDeep, int rowCount, int circuitCount)
        {
            Offset = offset;
            RowsDeep = rowsDeep;
            RowCount = rowCount;
            if (Offset)
            {
                InitOffsetTubes();
            }
            else
            {
                InitNoOffsetTubes();
            }

            void InitOffsetTubes()
            {
                TubeRows = new List<List<Tube>>();
                for (int i = 0; i < RowCount; i++)
                {
                    TubeRows.Add(new List<Tube>());
                    int currentRowDeep = i % 2 == 0 || RowsDeep % 2 == 0 ? RowsDeep : RowsDeep - 1;
                    for (int j = 0; j < currentRowDeep; j++)
                    {
                        TubeRows[i].Add(new Tube(i, j));
                    }
                }
            }

            void InitNoOffsetTubes()
            {
                TubeRows = new List<List<Tube>>();
                for (int i = 0; i < RowCount; i++)
                {
                    TubeRows.Add(new List<Tube>());
                    for (int j = 0; j < RowsDeep; j++)
                    {
                        TubeRows[i].Add(new Tube(i, j));
                    }
                }
            }
        }
    }
}
