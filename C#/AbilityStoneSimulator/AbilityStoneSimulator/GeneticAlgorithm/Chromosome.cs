using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbilityStoneSimulator.GeneticAlgorithm
{
    public class Chromosome
    {
        public double[] Genes { get; set; }
        public double FitnessScore { get; set; } = 0;
        public object ResultingObject { get; set; } = null;

        public Chromosome(double[] genes)
        {
            Genes = genes;
        }

        /// <summary>
        /// Normalizes the gene array to ensure a balanced genetic algorithm
        /// </summary>
        public void Normalize()
        {
            double magnitude = Math.Sqrt(Genes.Sum(t => t * t));

            for (int i = 0; i < Genes.Length; i++)
            {
                Genes[i] /= magnitude;
            }
        }
    }
}
