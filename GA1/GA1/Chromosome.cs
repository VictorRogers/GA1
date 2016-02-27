using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA1
{
    class Chromosome 
    {
        public string bits;
        public float fitness;

        public Chromosome()
        {
        }

        public Chromosome(string newBits, float newfitness)
        {
            bits = newBits;
            fitness = newfitness;
        }
    }
}
