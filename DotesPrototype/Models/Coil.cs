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
        public List<Tube> UnusedTubes{ get; set; }
        public CoilError Error { get; set; }
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
        public string Log { get
            {
                return $"Circuit Pattern: {Combination.ToString()}\n Total tubes: {TubesCount}\n Tubes used: {Combination.tubePatterns.Sum()} ";
            }
        }
        public List<int> CircuitCombination{ get; set; }
        public TubesCombination Combination { get; set; }

        public Coil(bool offset, int rowsDeep, int rowCount, int circuitCount)
        {
            try
            {
                Circuits = new List<Circuit>();
                UnusedTubes = new List<Tube>();
                Error = new CoilError();
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
                foreach (var item in TubeRows)
                {
                    UnusedTubes.AddRange(item);
                }
                if (TubesCount / circuitCount < rowsDeep)
                {
                    Error.Message = "Tubes per circuit lower then rows deep, impossible to draw";
                    Error.HasError = true;
                }
                Combination = new TubesCombination(TubesCount, circuitCount);
                GenerateRandomCircuits();
            }
            catch (Exception e)
            {
                Error.Message = e.Message;
                Error.HasError = true;
            }
        }
        private void InitOffsetTubes()
        {
            TubeRows = new List<List<Tube>>();
            for (int i = 0; i < RowCount; i++)
            {
                TubeRows.Add(new List<Tube>());
                int currentRowDeep = i % 2 == 0 || RowsDeep % 2 == 0 ? (RowsDeep+1)/2 : (RowsDeep - 1)/2;
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
        void GenerateRandomCircuits()
        {
            Random rnd = new Random();
            foreach (int tubeCount in Combination.tubePatterns)
            {
                Circuit circuit = new Circuit();
                for (int i =0; i < tubeCount; i++)
                {
                    Tube tubeToAdd = UnusedTubes[rnd.Next(UnusedTubes.Count)];
                    circuit.Add(tubeToAdd);
                    UnusedTubes.Remove(tubeToAdd);
                }
                Circuits.Add(circuit);
            }
        }
        void GenerateGoodCircuits()
        {
            foreach (int tubeCount in Combination.tubePatterns)
            {
                Circuit circuit = new Circuit();

            }
        }
        List<Tube> findPath(Tube start, int targetCount, int allowedSkip, int originalTargetCount)
        {
            List<Tube> rightOptions = GetRightTubes(start);
            bool isLast = rightOptions.Last().Column == TubeRows[rightOptions.Last().Row].Count - 1;
            if (rightOptions.Count == targetCount && isLast)
                return rightOptions;
            else if (rightOptions.Count == targetCount && !isLast)
                return null;
            if (rightOptions.Count == 0)
            {
                List<Tube> leftOptions = GetLeftTubes(start);
                if (leftOptions.Count > 0)
                {
                    List<List<Tube>> results = new List<List<Tube>>();
                    int counter = 0;
                    for (int i = leftOptions.Count; i >= 0; i--)
                    {
                        results = new List<List<Tube>>();
                        results[counter].AddRange(leftOptions);
                    }
                }

            }
            
            throw new NotImplementedException();
        }
        List<Tube> GetLeftTubes(Tube tube)
        {
            List<Tube> result = new List<Tube>();
            for (int i = 0; i < tube.Column; i++)
                if (UnusedTubes.Contains(TubeRows[tube.Row][i]))
                    result.Add(TubeRows[tube.Row][i]);
            return result;
        }
        List<Tube> GetRightTubes(Tube tube)
        {
            List<Tube> result = new List<Tube>();
            for (int i = tube.Column; i < TubeRows[tube.Row].Count; i++)
                if (UnusedTubes.Contains(TubeRows[tube.Row][i])&&TubeRows[tube.Row][i]!=tube)
                    result.Add(TubeRows[tube.Row][i]);
            return result;
        }
        List<Tube> GetBottomTubes(Tube tube, int allowedSkip)
        {
            if (tube.Row == TubeRows.Count - 1)
                return new List<Tube>();
            else
            {
                int startX = tube.Column - 1 - allowedSkip;
                int endX = tube.Column + 1 + allowedSkip;
                startX = startX < 0 ? 0 : startX;
                List<Tube> bottomRow = TubeRows[tube.Row + 1];
                endX = endX > bottomRow.Count - 1 ? bottomRow.Count - 1 : endX;

                List<Tube> result = new List<Tube>();
                for (int i = startX; i < endX; i++)
                    if (UnusedTubes.Contains(bottomRow[i]))
                        result.Add(bottomRow[i]);
                return result;
            }
        }
    }
}
