using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryAI
{
    public class Chromosome
    {
        public bool[] Genes;
        public float Fitness;

        public bool this[int index]
        {
            get
            {
                return Genes[index];
            }
            set
            {
                Genes[index] = value;
            }
        }

        public Chromosome(int geneCount)
        {
            Genes = new bool[geneCount];
            for (int i = 0; i < Genes.Length; i++)
            {
                Genes[i] = Common.random.NextDouble() <= Common.random.NextDouble();
            }
        }

    }
}
