using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA1
{
    class GASystem
    {
        public int chromosomeLength;
        public float crossoverRate;
        public int geneLength;
        public int maxGenerations;
        public float mutationRate;
        public int populationSize;


        public GASystem()
        {
        }

        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        //Returns a random float between 0 and 1
        public static float RandomFloat()
        {
            lock(syncLock)
            {
                return (float)random.NextDouble();
            }
        }


        public float AssignFitness(string bits, float targetVal)
        {
            if (bits != "")
            {
                //Holds decimal values of gene sequence
                int[] buffer = new int[(int)(chromosomeLength / geneLength)];
                int numElements = ParseBits(bits, buffer);

                float result = 0.0f;

                for (int i = 0; i < numElements - 1; i += 2)
                {
                    switch (buffer[i])
                    {
                        case 10:
                            result += buffer[i + 1];
                            break;

                        case 11:
                            result -= buffer[i + 1];
                            break;

                        case 12:
                            result *= buffer[i + 1];
                            break;

                        case 13:
                            result /= buffer[i + 1];
                            break;
                    }
                }

                if (result == targetVal)
                {
                    return 999.0f;
                }
                else
                {
                    return 1 / (float)Math.Abs((double)(targetVal - result));
                }
            }
            else //Invalid solution
            {
                return 0;
            }
        }


        //Converts a string of bits into a decimal integer
        public int BinToDec(string bits)
        {
            int val = 0;
            int valToAdd = 1;

            for (int i = bits.Length; i > 0; i--)
            {
                if (bits[i - 1] == '1')
                {
                    val += valToAdd;
                }
                valToAdd *= 2;
            }

            return val;
        }

        
        public void Crossover(ref string offspring1, ref string offspring2)
        {
            //Returns a random float between 0 and 1
            float randomNumber = RandomFloat();

            if (randomNumber < crossoverRate)
            {
                //Creating a random crossover point
                int crossover = (int)(randomNumber * chromosomeLength);

                if ((offspring1 != "") && (offspring2 != ""))
                {
                    StringBuilder t1 = new StringBuilder(offspring1.Substring(0, crossover) + offspring2.Substring(crossover, chromosomeLength - crossover));
                    StringBuilder t2 = new StringBuilder(offspring2.Substring(0, crossover) + offspring1.Substring(crossover, chromosomeLength  - crossover));
                    offspring1 = t1.ToString();
                    offspring2 = t2.ToString();                
                }
            }
        }


        public string GetRandomBits(int length)
        {
            string bits = "";

            for (int i = 0; i < length; i++)
            {
                //Returns a random float between 0 and 1
                float randomNumber = RandomFloat();
                if (randomNumber > 0.5f)
                {
                    bits += "1";
                }
                else
                {
                    bits += "0";
                }
            }

            return bits;
        }


        public int ParseBits(string bits, int[] buffer)
        {
            //Counter for buffer
            int cBuff = 0;
            //Determines if looking for an operator or a number
            bool bOperator = true;
            //Decimal value of gene currently being tested
            int thisGene = 0;

            for (int i = 0; i < chromosomeLength; i += geneLength)
            {
                //Converts current gene to a decimal
                thisGene = BinToDec(bits.Substring(i, geneLength));

                //Find a gene which represents an operator
                if (bOperator)
                {
                    if ((thisGene < 10) || (thisGene > 13))
                    {
                        continue;
                    }
                    else
                    {
                        bOperator = false;
                        buffer[cBuff++] = thisGene;
                        continue;
                    }
                }
                
                else
                {
                    if (thisGene > 9)
                    {
                        continue;
                    }

                    else
                    {
                        bOperator = true;
                        buffer[cBuff++] = thisGene;
                        continue;
                    }
                }
            }//Next gene
            
            //Check for a divide by zero in the chromosome
            //If so, change the / to a +
            for (int i = 0; i < cBuff; i++)
            {
                if ((buffer[i] == 13) && (buffer[i + 1] == 0))
                {
                    buffer[i] = 10;
                }
            }

            return cBuff;
        }


        //Outputs the converted integer value or symbol
        public void PrintGeneSymbol(int val)
        {
            if (val < 10)
            {
                Console.WriteLine(val + " ");
            }
            else
            {
                switch (val)
                {
                    case 10:
                        Console.WriteLine("+");
                        break;

                    case 11:
                        Console.WriteLine("-");
                        break;

                    case 12:
                        Console.WriteLine("*");
                        break;

                    case 13:
                        Console.WriteLine("/");
                        break;
                }
                Console.WriteLine(" ");
            }
        }


        //Decodes and outputs a chromosome to the console
        public void PrintChromosome(string bits)
        {
            int size = chromosomeLength / geneLength;
            int[] buffer = new int[size];

            int numElements = ParseBits(bits, buffer);

            for (int i = 0; i < numElements; i++)
            {
                PrintGeneSymbol(buffer[i]);
            }
        }


        public string Roulette(float totalFitness, Chromosome[] Population)
        {
            //Returns a random float between 0 and 1
            float randomNumber = RandomFloat();

            float fitnessSoFar = 0.0f;

            for (int i = 0; i < populationSize; i++)
            {
                fitnessSoFar += Population[i].fitness;

                if (fitnessSoFar > randomNumber)
                {
                    return Population[i].bits;
                }
            }

            return "";
        }


        public string Mutate(string bits)
        {
            StringBuilder sbNewBits = new StringBuilder(bits);
            string newBits;

            //Returns a random float between 0 and 1
            float randomNumber = RandomFloat();

            for (int i = 0; i < bits.Length; i++)
            {
                if (randomNumber < mutationRate)
                {
                    if (bits[i] == '1')
                    {
                        sbNewBits[i] = '0';
                    }
                    else
                    {
                        sbNewBits[i] = '1';
                    }
                }
            }

            newBits = sbNewBits.ToString(); 
            return newBits;
        }


        public void Start()
        {
            while (true)
            {
                //Population of Chromosomes
                Chromosome[] Population = new Chromosome[populationSize];

                for (int i = 0; i < populationSize; i++)
                {
                    Population[i] = new Chromosome();
                }

                //Target number to reach
                float target;
                Console.WriteLine("\nInput a target number: ");
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
                            Console.WriteLine("\nSolution found in " + GenerationsRequiredToFindASolution + " generations.\n\n");
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
                        Crossover(ref offspring1, ref offspring2);
                        offspring1 = Mutate(offspring1);
                        offspring2 = Mutate(offspring2);

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
                        Console.WriteLine("Failed to evolve a solution\n\n");
                        bFound = true;
                    }
                }
            }
        }
    }
}
