using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbilityStoneSimulator
{
    public static class Random
    {
        private static System.Random rand = new System.Random();

        /// <summary>
        /// Manually sets the seed for known results
        /// </summary>
        /// <param name="seed">Seed to set</param>
        public static void SetSeed(int seed) { rand = new System.Random(seed); }

        /// <summary>
        /// Random integer with lower(inclusive) and upper(exclusive) bounds.
        /// </summary>
        /// <param name="lowerBound">Inclusive lower bound</param>
        /// <param name="upperBound">Exclusive upper bound</param>
        /// <returns>A random integer</returns>
        public static int Int(int lowerBound, int upperBound) => rand.Next(lowerBound, upperBound);

        /// <summary>
        /// Random float with lower(inclusive) and upper(exclusive) bounds.
        /// </summary>
        /// <param name="lowerBound">Inclusive lower bound</param>
        /// <param name="upperBound">Exclusive upper bound</param>
        /// <returns>A random float</returns>
        public static float Float(float lowerBound, float upperBound)
            => ((float)rand.NextDouble() * (upperBound - lowerBound)) + lowerBound;

        /// <summary>
        /// Random float between 0(inclusive) and 1(exclusive)
        /// </summary>
        /// <returns></returns>
        public static float Float() => (float)rand.NextDouble();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="percentageChanceToBeat">0-100</param>
        /// <returns></returns>
        public static bool RollPercentageChance(int percentageChanceToBeat) 
            => RollPercentageChance((float)percentageChanceToBeat / 100f);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="percentageChanceToBeat"></param>
        /// <returns></returns>
        public static bool RollPercentageChance(float percentageChanceToBeat)
        {
            if (percentageChanceToBeat < 0f)
            {
                percentageChanceToBeat = 0f;
            }
            else if (percentageChanceToBeat > 1f)
            {
                percentageChanceToBeat = 1f;
            }

            return Float() <= percentageChanceToBeat;
        }
    }
}
