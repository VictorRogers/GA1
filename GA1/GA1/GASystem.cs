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
            string bits;
            //Random number probably needs to be done at a higher scope due to seeding issues
            Random random = new Random();
            float randomNumber = random.Next(0, 1);

            for (int i = 0; i < length; i++)
            {
                if ()
            }
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

        public string Roulette(float totalFitness, Chromosome Population)
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
            Chromosome[] Population = new Chromosome[populationSize];

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

                //Test and update fitness for all chromosomes
                for (int i = 0; i < populationSize; i++)
                {
                    Population[i].fitness = AssignFitness(Population[i].bits, target);
                    TotalFitness += Population[i].fitness;
                }

                //Check to see if a solution is found
                for (int i = 0; i < populationSize; i++)
                {
                    if (Population[i].fitness == 999.0f)
                    {
                        Console.WriteLine("\nSolution found in " + GenerationsRequiredToFindASolution + " gnerations.\n\n");
                        PrintChromosome(Population[i].bits);
                        bFound = true;
                        i = populationSize;
                    }
                }

                //Creating a new population

                //Temp storage for new population
                Chromosome[] TemporaryPopulation = new Chromosome[populationSize];
                int cPopulation = 0;

                while (cPopulation < populationSize)
                {
                    string offspring1 = Roulette(TotalFitness, Population);
                    string offspring2 = Roulette(TotalFitness, Population);

                    //Crossover and mutate
                    Crossover(offspring1, offspring2);
                    Mutate(offspring1);
                    Mutate(offspring2);

                    //Add offspring into the new population
                    TemporaryPopulation[cPopulation++] = new Chromosome(offspring1, 0.0f);
                    TemporaryPopulation[cPopulation++] = new Chromosome(offspring2, 0.0f);
                }

                //Moving temp population into the main population
                for (int i = 0; i < populationSize; i++)
                {
                    Population[i] = TemporaryPopulation[i];
                }

                GenerationsRequiredToFindASolution++;

                if (GenerationsRequiredToFindASolution > maxGenerations)
                {
                    Console.WriteLine("Failed to evolve a solution\n");
                    bFound = true;
                }
            }
        }
    }
}
