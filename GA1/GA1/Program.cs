using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA1
{
    class Program
    {
        static void Main(string[] args)
        {
            GASystem GASystem = new GASystem();

            GASystem.chromosomeLength = 300;
            GASystem.crossoverRate = 0.7f;
            GASystem.geneLength = 4;
            GASystem.maxGenerations = 400;
            GASystem.mutationRate = 0.001f;
            GASystem.populationSize = 100;

            GASystem.Start();
        }
    }
}
