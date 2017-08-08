using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace GeometryAI
{
    class Program
    {
        [DllImport("PercentLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Init();

        [DllImport("PercentLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern float GetPositionX();


        static GeneticAlgorithm ga;
        static float GetBestFitness()
        {
            float bestFitness = 0;

            for (int i = 0; i < ga.Populations.Count; i++)
            {
                Chromosome chromo = ga.Populations[i];

                if (chromo.Fitness > bestFitness)
                {
                    bestFitness = chromo.Fitness;
                }
            }

            return bestFitness;
        }

        const int GeneCount = 30;
        static void Main(string[] args)
        {
            for (int i = 3; i >= 0; i--)
            {
                Console.WriteLine($"{i}...");
                Thread.Sleep(1000);
            }
            Init();

            ga = new GeneticAlgorithm(GeneCount);
            ga.AddRandomPopulation(20);

            MouseMacro macro = new MouseMacro();
            float xpos = 0;
            float last_xpos = 0;
            float delta_xpos = 0;
            float max_xpos = 0;
            float time = 0;

            int chromoIndex = 0;
            int geneIndex = 0;

            bool toggle = false;

            macro.LeftDown();

            while (true)
            {
                xpos = GetPositionX();
                max_xpos = Math.Max(xpos, max_xpos);
                delta_xpos = xpos - last_xpos;

                if (delta_xpos < 0 || geneIndex > ga.GeneCount - 1)
                {
                    geneIndex = 0;

                    float fitness = max_xpos;
                    ga.Populations[chromoIndex].Fitness = fitness;
                    max_xpos = 0;
                    chromoIndex++;
                    Console.WriteLine("Next Chromo : {0} / {1} | Fitness: {2:F2}", chromoIndex, ga.Populations.Count - 1, fitness);

                    if (chromoIndex > ga.Populations.Count - 1)
                    {
                        float bestFitness = GetBestFitness();

                        chromoIndex = 0;
                        ga.Select(5);
                        ga.CrossOver(10, 0.5);
                        ga.Mutation(0.001);
                        ga.Generation++;

                        
                        Console.WriteLine("Generation: {0} / Best Fitness: {1}", ga.Generation, bestFitness);
                    }
                }

                Chromosome chromo = ga.Populations[chromoIndex];
                time += 0.02f;
                if (time > chromo.Genes[geneIndex])
                {
                    time = 0;
                    geneIndex++;
                    toggle = !toggle;
                    if (toggle)
                    {
                        macro.LeftUp();
                    } else
                    {
                        macro.LeftDown();
                    }
                }
                last_xpos = GetPositionX();
                Thread.Sleep(20);
            }
        }
    }
}
