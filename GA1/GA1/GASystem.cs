using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA1
{
    class GASystem
    {
        float crossoverRate;
        float mutationRate;
        int populationSize;
        int chromosomeLength;
        int geneLength;
        int maxGenerations;

        public GASystem()
        {
            chromosomeLength = 300;
            populationSize = 100;
        }

        public void PrintGeneSymbol(int val)
        {
        }

        public string GetRandomBits(int length)
        {
            string a = "0";
            return a;
        }

        public int BinToDec(string bits)
        {
            int a = 0;
            return a;
        }

        public float AssignFitness(string bits, float targetVal)
        {
            float a = 0;
            return a;
        }

        public void PrintChromosome(string bits)
        {
        }

        public string Roulette(int totalFitness, Chromosome Population)
        {
            string a = "0";
            return a;
        }

        public void Mutate(string bits)
        {
        }

        public void Crossover(string offspring1, string offspring2)
        {
        }

        public void Start()
        {
            //Population of Chromosomes
            Chromosome[] Population = new Chromosome[100];

            //Target number to reach
            float target;
            Console.WriteLine("Input a target number: ");
            target = float.Parse(Console.ReadLine());

            //Building a random population with null fitness
            for (int i = 0; i < populationSize; i++)
            {
                Population[i].bits = GetRandomBits(chromosomeLength);
                Population[i].fitness = 0.0f;
            }

            int GenerationsRequiredToFindASolution = 0;

            //Solution found flag
            bool bFound = false;

            //Main Genetic Algorithm loop
            while (!bFound)
            {
                float TotalFitness = 0.0f;

                for (int i = 0; i < populationSize; i++)
                {
                    Population[i].fitness = AssignFitness(Population[i].bits, target);
                    TotalFitness += Population[i].fitness;
                }

                for (int i = 0; i < populationSize; i++)
                {
                    if (Population[i].fitness = 999.0f)
                    {
                        Console.WriteLine("\nSolution found in " + GenerationsRequiredToFindASolution + " gnerations.\n\n");
                        PrintChromosome(Population[i].bits);
                        bFound = true;
                        i = populationSize;
                    }
                }
            }
        }
    }
}
