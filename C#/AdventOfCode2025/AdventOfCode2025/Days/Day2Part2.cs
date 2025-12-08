//---Part Two-- -
//The clerk quickly discovers that there are still invalid IDs in the ranges in your list. Maybe the young Elf was doing other silly patterns as well?

//Now, an ID is invalid if it is made only of some sequence of digits repeated at least twice. So, 12341234 (1234 two times), 123123123 (123 three times), 1212121212 (12 five times), and 1111111 (1 seven times) are all invalid IDs.

//From the same example as before:

//11 - 22 still has two invalid IDs, 11 and 22.
//95-115 now has two invalid IDs, 99 and 111.
//998-1012 now has two invalid IDs, 999 and 1010.
//1188511880-1188511890 still has one invalid ID, 1188511885.
//222220-222224 still has one invalid ID, 222222.
//1698522-1698528 still contains no invalid IDs.
//446443-446449 still has one invalid ID, 446446.
//38593856-38593862 still has one invalid ID, 38593859.
//565653-565659 now has one invalid ID, 565656.
//824824821-824824827 now has one invalid ID, 824824824.
//2121212118-2121212124 now has one invalid ID, 2121212121.
//Adding up all the invalid IDs in this example produces 4174379265.

//What do you get if you add up all of the invalid IDs using these new rules?

namespace AdventOfCode2025.Days
{
    internal static class Day2Part2
    {
        public static string Solve(List<string> data)
        {
            long sum = 0;
            foreach (string range in data)
            {
                long start = long.Parse(range.Split('-')[0]);
                long end = long.Parse(range.Split('-')[1]);
                for (long i = start; i <= end; i++)
                {
                    bool result = DoesSequenceHaveRepeatingValue(i);
                    if (result)
                    {
                        sum += i;
                    }
                }
            }
            return sum.ToString();
        }

        private static bool DoesSequenceHaveRepeatingValue(long sequence)
        {
            string str = sequence.ToString();
            int maxSegmentLength = str.Length / 2;
            for (int i = 1; i <= maxSegmentLength; i++)
            {
                if (str.Length % i != 0)
                {
                    continue;
                }

                string[] result = SplitStringIntoEqualLengths(str, i);
                bool result2 = AreAllStringsInArraySame(result);
                if (result2)
                {
                    return true;
                }
            }
            return false;
        }

        private static string[] SplitStringIntoEqualLengths(string str, int segmentLength)
        {
            if (str.Length % segmentLength != 0) 
            {
                throw new Exception($"String {str} must be divisible by segmentLength {segmentLength}");
            }

            List<string> strings = new List<string>();
            for (int i = 0; i < str.Length / segmentLength; i++)
            {
                strings.Add(str.Substring(i * segmentLength, segmentLength));
            }
            return strings.ToArray();
        }

        private static bool AreAllStringsInArraySame(string[] strings)
        {
            for (int i = 0; i < strings.Length - 1; i++)
            {
                for (int j = i + 1; j < strings.Length; j++)
                {
                    if (strings[i] != strings[j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
