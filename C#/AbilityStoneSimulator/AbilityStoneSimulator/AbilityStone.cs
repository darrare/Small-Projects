using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbilityStoneSimulator
{
    public class AbilityStone
    {
        public AbilityStoneEngraving FirstGood { get; set; } = new AbilityStoneEngraving();
        public AbilityStoneEngraving SecondGood { get; set; } = new AbilityStoneEngraving();
        public AbilityStoneEngraving Bad { get; set; } = new AbilityStoneEngraving();

        // Stats
        public int FirstEngravingScore { get; private set; }
        public int SecondEngravingScore { get; private set; }
        public int MaliceScore { get; private set; }
        public int TotalPositiveScore { get; private set; }

        public AbilityStone()
        {

        }

        public AbilityStone DeepCopyThis()
        {
            AbilityStone newStone = new AbilityStone();
            newStone.FirstGood = FirstGood.DeepCopyThis();
            newStone.SecondGood = SecondGood.DeepCopyThis();
            newStone.Bad = Bad.DeepCopyThis();

            newStone.FinalizeStone();
            return newStone;
        }

        private void Reset()
        {
            FirstGood = new AbilityStoneEngraving();
            SecondGood = new AbilityStoneEngraving();
            Bad = new AbilityStoneEngraving();
            FirstEngravingScore = 0;
            SecondEngravingScore = 0;
            MaliceScore = 0;
            TotalPositiveScore = 0;
        }

        public void FinalizeStone()
        {
            FirstEngravingScore = FirstGood.Scores.Count(t => t == true);
            SecondEngravingScore = SecondGood.Scores.Count(t => t == true);
            MaliceScore = Bad.Scores.Count(t => t == true);
            TotalPositiveScore = FirstEngravingScore + SecondEngravingScore;
        }

        public bool RollFirstGood(int currentPercentage)
        {
            bool result = Random.RollPercentageChance(currentPercentage);
            FirstGood.SetNext(result);
            return result;
        }
        public bool RollSecondGood(int currentPercentage)
        {
            bool result = Random.RollPercentageChance(currentPercentage);
            SecondGood.SetNext(result);
            return result;
        }
        public bool RollBad(int currentPercentage)
        {
            bool result = Random.RollPercentageChance(currentPercentage);
            Bad.SetNext(result);
            return result;
        }

        public override string ToString()
        {
            return $"Pos: {TotalPositiveScore}. Neg: {MaliceScore}";
        }

        /// <summary>
        /// The fitness is simply the best possible outcome from our given position
        /// </summary>
        /// <param name="genes">
        /// Genes to calculate on.
        /// 0. dist1 (1 = dist1 + dist2 + dist3)
        /// 1. dist2 (1 = dist1 + dist2 + dist3)
        /// 2. dist3 (1 = dist1 + dist2 + dist3)
        /// 3. percentage1
        /// 4. percentage2
        /// 5. percentage3
        /// 6. dist weight
        /// </param>
        /// <param name="seed">Seed to generate based on</param>
        /// <returns></returns>
        public dynamic Fitness(double[] genes, int seed)
        {
            if (seed != 0)
                Random.SetSeed(seed);

            int currentPercentage = 75;

            for (int iteration = 0; iteration < 30; iteration++)
            {
                // Run the logic through using the genes to determine which to update in what order.
                double dist1 = (10 - FirstGood.Counter) * genes[0];
                double dist2 = (10 - SecondGood.Counter) * genes[1];
                double dist3 = (10 - Bad.Counter) * genes[2];

                double percentage1 = FirstGood.Potential * ((currentPercentage - 25) / 50) * genes[3];
                double percentage2 = SecondGood.Potential * ((currentPercentage - 25) / 50) * genes[4];
                double percentage3 = Bad.BadPotential * (1 - ((currentPercentage - 25) / 50)) * genes[5];

                // Given the potential, the genes, and the current percentage, which do I want to roll most
                double desireToRoll1 = dist1 * genes[6] + percentage1;
                double desireToRoll2 = dist2 * genes[6] + percentage2;
                double desireToRoll3 = dist3 * genes[6] + percentage2;

                List<Tuple<int, double>> sorted = new List<Tuple<int, double>>()
                {
                    new Tuple<int, double>(1, desireToRoll1),
                    new Tuple<int, double>(2, desireToRoll2),
                    new Tuple<int, double>(3, desireToRoll3)
                }.OrderBy(t => t.Item2).ToList();

                // Roll based on heuristic
                bool hasRolled = false;
                for (int i = 0; i < sorted.Count; i++)
                {
                    switch (sorted[i].Item1)
                    {
                        case 1:
                            if (FirstGood.Counter < 10)
                            {
                                updatePercentageChance(RollFirstGood(currentPercentage));
                                hasRolled = true;
                            }
                            break;
                        case 2:
                            if (SecondGood.Counter < 10)
                            {
                                updatePercentageChance(RollSecondGood(currentPercentage));
                                hasRolled = true;
                            }
                            break;
                        case 3:
                            if (Bad.Counter < 10)
                            {
                                updatePercentageChance(RollBad(currentPercentage));
                                hasRolled = true;
                            }
                            break;
                        default:
                            throw new Exception("This isn't possible... Sorting on non 1,2,3");
                    }
                    // If it rolls once, break out
                    if (hasRolled)
                        break;
                }
            }


            // Then return the positive minus the negative, weighted. Obviously 9/7/4 is better than 8/7/2
            FinalizeStone();
            double fitness = TotalPositiveScore * 2 - MaliceScore;
            object output = DeepCopyThis();
            Reset();
            return new { fitness, output, genes }; 

            void updatePercentageChance(bool wasSuccessful)
            {
                if (wasSuccessful)
                {
                    currentPercentage -= 10;
                }
                else
                {
                    currentPercentage += 10;
                }

                if (currentPercentage < 25)
                {
                    currentPercentage = 25;
                }
                else if (currentPercentage > 75)
                {
                    currentPercentage = 75;
                }
            }
        }
    }

    public class AbilityStoneEngraving
    {
        public bool?[] Scores { get; set; } = new bool?[10];
        public int Counter { get; set; } = 0;

        public int Potential { get { return 10 - Counter + Scores.Count(t => t == true); } }
        public int BadPotential { get { return 10 - Counter + Scores.Count(t => t == false); } }

        public bool? this[int i]
        {
            get { return Scores[i]; }
            set { Scores[i] = value; }
        }

        public AbilityStoneEngraving DeepCopyThis()
        {
            AbilityStoneEngraving newEngraving = new AbilityStoneEngraving();

            for (int i = 0; i < Scores.Length; i++)
            {
                newEngraving[i] = Scores[i];
            }

            newEngraving.Counter = Counter;

            return newEngraving;
        }

        public void SetNext(bool result)
        {
            Scores[Counter] = result;
            Counter++;
        }
    }
}
