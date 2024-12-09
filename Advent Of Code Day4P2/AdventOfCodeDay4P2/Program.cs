namespace AdventOfCodeD4P2
{
    //using OO programming
    class Program
    {
        static void Main(string[] args)
        {
            List<string> lines = new List<string>();
            while (Console.ReadLine() is string line)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    lines.Add(line);
                }
                if (string.IsNullOrEmpty(line))
                {
                    Counter counter = new Counter();
                    Console.WriteLine(counter.CountCrossMasAppearances(lines));
                }
            }
        }
    }
}
