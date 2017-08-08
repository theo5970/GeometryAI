using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryAI
{
    public class Chromosome
    {
        public float[] Genes;
        public float Fitness;

        public float this[int index]
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
            Genes = new float[geneCount];
            for (int i = 0; i < Genes.Length; i++)
            {
                if (i % 2 == 0)
                {
                    Genes[i] = (float)Common.random.NextDouble() * 1f;
                } else
                {
                    Genes[i] = (float)Common.random.NextDouble() * 2;
                }
            }
        }

    }
}
