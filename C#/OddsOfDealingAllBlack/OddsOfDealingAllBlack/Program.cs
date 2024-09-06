int iterationsPerThread = 1000000000;
int numThreads = 10;
Task<int>[] tasks = new Task<int>[numThreads];
int[] winResults = new int[numThreads];

for (int i = 0; i < numThreads; i++)
{
    tasks[i] = Task<int>.Run(() =>
    {
        List<Tuple<int, int>> deck = new List<Tuple<int, int>>();
        for (int color = 0; color < 4; color++)
        {
            for (int num = 1; num <= 9; num++)
            {
                deck.Add(new Tuple<int, int>(color, num));
            }
        }
        deck.Add(new Tuple<int, int>(4, 1));
        deck.Add(new Tuple<int, int>(4, 2));
        deck.Add(new Tuple<int, int>(4, 3));
        deck.Add(new Tuple<int, int>(4, 4));

        return deck.RunAndCollectWins(iterationsPerThread);
    });
}

for (int i = 0; i < numThreads; i++)
{
    tasks[i].Wait();
}

int numWins = tasks.Sum(t => t.Result);


Console.WriteLine($"Won {numWins} out of {iterationsPerThread * numThreads}");
Console.WriteLine($"Odds = {((float)numWins / ((float)iterationsPerThread * (float)numThreads)) * 100}%");
Console.ReadKey();

public static class Extensions
{
    // Fisher-Yates shuffle
    public static void Shuffle(this List<Tuple<int, int>> deck)
    {
        Random rand = new Random();
        int n = deck.Count;
        while (n > 1)
        {
            n--;
            int index = rand.Next(n + 1);
            Tuple<int, int> value = deck[index];
            deck[index] = deck[n];
            deck[n] = value;
        }
    }

    public static int RunAndCollectWins(this List<Tuple<int, int>> deck, int iterations)
    {
        int losses = 0;
        for (int runNum = 0; runNum < iterations; runNum++)
        {
            deck.Shuffle();
            Dictionary<int, int> players = new Dictionary<int, int>
            {
                { 0, 0 },
                { 1, 0 },
                { 2, 0 },
                { 3, 0 },
                { 4, 0 },
            };
            for (int index = 0; index < deck.Count; index++)
            {
                if (deck[index].Item1 == 4)
                {
                    players[index % 5]++;
                    // Exit if someone else already has a black
                    if (players.Any(t => t.Key != index % 5 && t.Value > 0))
                    {
                        losses++;
                        break;
                    }
                }
            }
        }

        return iterations - losses;
    }
}

