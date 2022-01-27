using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbilityStoneSimulator.GeneticAlgorithm;

namespace AbilityStoneSimulator
{
    public class AbilityStoneRoller
    {
        int currentPercentageChance = 75;

        public AbilityStoneRoller()
        {

        }

        private void updatePercentageChance(bool wasSuccessful)
        {
            if (wasSuccessful)
            {
                currentPercentageChance -= 10;
            }
            else
            {
                currentPercentageChance += 10;
            }

            if (currentPercentageChance < 25)
            {
                currentPercentageChance = 25;
            }
            else if (currentPercentageChance > 75)
            {
                currentPercentageChance = 75;
            }
        }

        public RollerResult HardPercentageAlgorithm_EngravingsEqual(int n, int lessThanThisRollBad)
        {
            RollerResult results = new RollerResult();

            for (int i = 0; i < n; i++)
            {
                AbilityStone stone = new AbilityStone();
                for (int x = 0; x < 30; x++) //limit to 30 rolls
                {
                    if (currentPercentageChance < lessThanThisRollBad
                        && stone.Bad.Counter < 10)
                    {
                        updatePercentageChance(stone.RollBad(currentPercentageChance));
                    }
                    else if (stone.FirstGood.Counter <= stone.SecondGood.Counter
                        && stone.FirstGood.Counter < 10)
                    {
                        updatePercentageChance(stone.RollFirstGood(currentPercentageChance));
                    }
                    else if (stone.FirstGood.Counter > stone.SecondGood.Counter
                        && stone.SecondGood.Counter < 10)
                    {
                        updatePercentageChance(stone.RollSecondGood(currentPercentageChance));
                    }
                    else if (stone.Bad.Counter < 10)
                    {
                        updatePercentageChance(stone.RollBad(currentPercentageChance));
                    }
                    else
                    {
                        throw new Exception("This algorithm should never hit this else.");
                    }
                }
                stone.FinalizeStone();
                results.Add(stone);
            }

            return results;
        }


        public RollerResult RunGeneticAlgorithm(int n, int firstGoal = 9, int secondGoal = 7)
        {
            AbilityStone stone = new AbilityStone();
            int seed = 0;

            List<Chromosome> results = GeneticAlgorithm.GeneticAlgorithm.RunGeneticAlgorithm(
                528,
                .1,
                new double[] { 1, 1, 1, 1, 1, 1, 1 },
                .5,
                .5,
                .5,
                stone.Fitness,
                1000,
                true,
                seed);

            // Take the top 5 or so results and generate AbilityStone's with the genes to find which has consistent results.

            List<dynamic> regeneratedResults = new List<dynamic>();
            foreach (Chromosome item in results.Take(5))
            {
                for (int i = 0; i < 32; i++)
                {
                    regeneratedResults.Add(stone.Fitness(item.Genes, seed));
                }
            }

            regeneratedResults = regeneratedResults.OrderByDescending(t => t.fitness).ToList();


            return null;
        }

    }

    public class RollerResult
    {
        public List<AbilityStone> Stones { get; set; } = new List<AbilityStone>();

        int maxResult;
        public int MaxResult { 
            get
            {
                if (maxResult == 0)
                    maxResult = Stones.Max(t => t.TotalPositiveScore);
                return maxResult;
            }
        }
        public List<AbilityStone> BestResults() => Stones.Where(t => t.TotalPositiveScore == maxResult).ToList();
        public void Add(AbilityStone stone)
        {
            Stones.Add(stone);
        }
    }
}
