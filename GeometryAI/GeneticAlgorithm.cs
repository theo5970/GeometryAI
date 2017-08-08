using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryAI
{
    public class GeneticAlgorithm
    {
        public List<Chromosome> Populations;

        public long Generation;

        public int GeneCount
        {
            get;
        }

        public GeneticAlgorithm(int geneCount)
        {
            GeneCount = geneCount;
            Populations = new List<Chromosome>();
        }

        const float K = 3;
        private float GetRealFitness(float fitness, float worst, float best)
        {
            return (worst - fitness) + (worst - best) / (K - 1);
        }

        public float GetWorstFitness()
        {
            float min = float.MaxValue;
            for (int i = 0; i < Populations.Count; i++)
            {
                Chromosome chromo = Populations[i];
                if (chromo.Fitness < min)
                {
                    min = chromo.Fitness;
                }
            }
            return min;
        }

        public float GetBestFitness()
        {
            float max = float.MinValue;
            for (int i = 0; i < Populations.Count; i++)
            {
                Chromosome chromo = Populations[i];
                if (chromo.Fitness > max)
                {
                    max = chromo.Fitness;
                }
            }
            return max;
        }

        public void AddRandomPopulation(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Populations.Add(new Chromosome(GeneCount));
            }
        }

        private int RouletteWheel()
        {
            int selected = 0;
            float total = 0;

            float worstFitness = GetWorstFitness();
            float bestFitness = GetBestFitness();


            for (int i = 0; i < Populations.Count; i++)
            {
                Chromosome chromo = Populations[i];
                float fitness = GetRealFitness(chromo.Fitness, worstFitness, bestFitness);
                total += fitness;

                if (Common.random.NextDouble() <= (fitness / total))
                {
                    selected = i;
                }
            }

            return selected;
        }

        public void Select(int count)
        {
            int lastCount = Populations.Count;

            for (int i = 0; i < count; i++)
            {
                int index = RouletteWheel();
                Populations.Add(Populations[index]);
            }
            Populations.RemoveRange(0, lastCount);
        }

        public void CrossOver(int count, double percent)
        {
            int lastCount = Populations.Count;
            for (int i = 0; i < count; i++)
            {
                Chromosome chromoA = Populations[Common.random.Next(lastCount)];
                Chromosome chromoB = Populations[Common.random.Next(lastCount)];

                Chromosome newChromo = new Chromosome(GeneCount);

                for (int j = 0; j < GeneCount; j++)
                {
                    if (Common.random.NextDouble() <= percent)
                    {
                        newChromo[j] = chromoA[j] && chromoB[j];
                    } else
                    {
                        newChromo[j] = Common.random.NextDouble() <= percent ? chromoA[j] : chromoB[j];
                    }
                }
                Populations.Add(newChromo);
            }

            Populations.RemoveRange(0, lastCount);

        }

        public void Mutation(double percent)
        {
            for (int i = 0; i < Populations.Count; i++)
            {
                Chromosome chromo = Populations[i];
                for (int j = 0; j < GeneCount; j++)
                {
                    if (Common.random.NextDouble() <= percent)
                    {
                        chromo[j] = !chromo[j];
                    }
                }
            }
        }
    }
}
