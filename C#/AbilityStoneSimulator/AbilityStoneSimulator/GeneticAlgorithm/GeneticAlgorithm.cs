using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbilityStoneSimulator.GeneticAlgorithm
{
    public delegate dynamic FitnessAlgorithm(double[] genes, int seed);

    public static class GeneticAlgorithm
    {
        /// <summary>
        /// Runs the genetic algorithm using the given parameters to solve for a list of Chromosomes that satisfy the problem
        /// </summary>
        /// <param name="populationSize">Total size of the population
        /// <para/>(must be in the form of Size = X + Y where x = y(y-1)/2).
        /// <para/>Example: Set Y = 100, therefore X = 100(99)/2 = 4950. X + Y = 5050 where Y is the number of parents we keep per iteration.)
        /// <para/>Some valid values: 36, 136, 528, 2080</param>
        /// <param name="maxConvergenceDeviationToAccept">0 means every chromosome produces the exact same result
        /// <para/>.01 means the worst chromosome is within 1% of the best chromosome
        /// <para/>(1 - min fitness score / max fitness score)</param>
        /// <param name="defaultGenes">Default gene set, determined by specific CSP</param>
        /// <param name="chanceToSelectEachChromosome">% chance for each chromosome to be selected for mutation</param>
        /// <param name="chanceToMutateEachGene">% chance for each gene in the selected chromosomes to be mutated</param>
        /// <param name="maxGeneMutationDeviation">maximum % amount a genes value can change after a mutation
        /// <para/>.5 means +- 50%, IE: 90 -> 45-135 and 180 -> 90-270
        /// <para/>1 means the mutation will range from 0-double whatever the genes value was</param>
        /// <param name="fitnessAlgorithm">The algorithm provided by the CSP to determine the success of the chromosome</param>
        /// <param name="maxIterationCount">Maximum number of evolutions. Prevents algorithm from running forever when fitness scores won't converge. Default is 1000</param>
        /// <param name="isHigherFitnessBetter">Determines how to order the results.</param>
        /// <param name="seed">random seed</param>
        /// <returns>List of chromosomes that was the last set before a convergence was found, ordered from highest to lowest</returns>
        public static List<Chromosome> RunGeneticAlgorithm(
            int populationSize,
            double maxConvergenceDeviationToAccept,
            double[] defaultGenes,
            double chanceToSelectEachChromosome,
            double chanceToMutateEachGene,
            double maxGeneMutationDeviation,
            FitnessAlgorithm fitnessAlgorithm,
            int maxIterationCount = 1000,
            bool isHigherFitnessBetter = true,
            int seed = 0)
        {
            //Creates a new population, automatically mutates each gene by up to maxGeneMutationDeviation
            Population pop = new Population(populationSize, defaultGenes, maxGeneMutationDeviation, isHigherFitnessBetter, seed);

            double convergence = 0;
            double averageFitness = 0;
            double maximumFitness = 0;
            double minimumFitness = 0;
            int i;
            for (i = 0; i < maxIterationCount; i++)
            {
                pop.CalculateFitness(fitnessAlgorithm, seed);
                if ((convergence = pop.CalculateConvergence()) <= maxConvergenceDeviationToAccept)
                {
                    averageFitness = pop.CalculateAverageFitness();
                    minimumFitness = pop.CalculateMinimumFitness();
                    maximumFitness = pop.CalculateMaximumFitness();
                    break;
                }

                pop.RemoveUnworthy();
                pop.MatePopulation();
                pop.Mutate(chanceToSelectEachChromosome, chanceToMutateEachGene, maxGeneMutationDeviation);
            }

            if (isHigherFitnessBetter)
                return pop.Chromosomes.OrderByDescending(t => t.FitnessScore).ToList();
            else
                return pop.Chromosomes.OrderBy(t => t.FitnessScore).ToList();
        }
    }

    public class FitnessResults
    {
        public FitnessResults(List<Tuple<Chromosome, double>> fitnesses)
        {

        }
    }
}
