using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotesPrototype.Models
{
    //TO DO: make evenly distribution of numbers, like here for example https://stackoverflow.com/questions/57931009/evenly-distribute-values-into-array
    public class TubesCombination
    {
        public int TubesCount { get; set; }
        public int CircuitCount { get; set; }
        private int rawTubesPerCircuit;
        public int FirstNumber { get; set; }
        public int SecondNumber { get; set; }
        public List<int> ResultCombination { get; set; }
        public TubesCombination(int tubes, int circuits)
        {
            TubesCount = tubes;
            CircuitCount = circuits;
            rawTubesPerCircuit = TubesCount / CircuitCount;
            CircuitCombinations();
        }
        private void CircuitCombinations()
        {
            if ((TubesCount%CircuitCount) == 0 && rawTubesPerCircuit % 2 == 0)
            {
                ResultCombination = new List<int>();
                for (int i = 0; i < CircuitCount; i++)
                    ResultCombination.Add(rawTubesPerCircuit);
            }
            else
            {
                if (rawTubesPerCircuit % 2 == 0)
                {
                    FirstNumber = rawTubesPerCircuit;
                    SecondNumber = rawTubesPerCircuit + 2;
                }
                else
                {
                    FirstNumber = rawTubesPerCircuit - 1;
                    SecondNumber = rawTubesPerCircuit + 1;
                }
                ResultCombination = findCombination(FirstNumber, SecondNumber);
            }
        }
        List<int> findCombination(int first,int second)
        {
            int min = Math.Min(first, second);
            int max = Math.Max(first, second);
            int closetSum = 0;
            int firstMul = CircuitCount;
            int secondMul = 0;
            for (int i = 0; i < CircuitCount; i++)
            {
                int firstMulTmp = CircuitCount - i;
                int secondMulTmp = CircuitCount - firstMulTmp;
                int newSum = firstMulTmp * min + secondMulTmp * max;
                if (newSum<TubesCount&& (TubesCount - closetSum > TubesCount - newSum))
                {
                    closetSum = newSum;
                    firstMul = firstMulTmp;
                    secondMul = secondMulTmp;
                }
            }
            List<int> result= new List<int>();
            for (int i = 0; i < secondMul; i++)
                result.Add(max);
            for (int i = 0; i < firstMul; i++)
                result.Add(min);
            return result;
        }
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append('[');
            foreach (var item in ResultCombination)
            {
                result.Append(item);
                result.Append(' ');
            }
            result.Append(']');
            return result.ToString();
        }
    }
}
