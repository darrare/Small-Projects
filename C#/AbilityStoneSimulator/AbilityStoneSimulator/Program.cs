using System;
using System.Linq;
using System.Collections.Generic;

namespace AbilityStoneSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            //AbilityStoneRoller roller = new AbilityStoneRoller();
            //RollerResult results = roller.HardPercentageAlgorithm_EngravingsEqual(1000, 35);
            //results.Stones = results.Stones.OrderByDescending(t => t.TotalPositiveScore).ToList();

            new AbilityStoneRoller().RunGeneticAlgorithm(528);
            Console.ReadLine();
        }

        static void FindProbabilityOf9SuccessesAtSeventyFivePercent()
        {
            DateTime start = DateTime.Now;
            Dictionary<int, int> successCountDict = new Dictionary<int, int>()
            {
                { 0, 0 },
                { 1, 0 },
                { 2, 0 },
                { 3, 0 },
                { 4, 0 },
                { 5, 0 },
                { 6, 0 },
                { 7, 0 },
                { 8, 0 },
                { 9, 0 },
                { 10, 0 },
            };
            for (int i = 0; i < 10000000; i++)
            {
                int successCount = 0;
                for (int x = 0; x < 10; x++)
                {
                    if (Random.Float() <= .75f)
                        successCount++;
                }
                successCountDict[successCount]++;
            }
            DateTime end = DateTime.Now;
            TimeSpan elapsed = end - start;
            var milliseconds = elapsed.Milliseconds;
        }
    }
}
