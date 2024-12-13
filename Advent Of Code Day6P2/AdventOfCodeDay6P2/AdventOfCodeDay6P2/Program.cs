namespace AdventOfCodeD6P2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> map = [];
            while (Console.ReadLine() is string row)
            {
                if (!string.IsNullOrEmpty(row) && row != "crimus")
                {
                    map.Add(row);
                }
                if (row == "crimus")
                {
                    //the code is a mess but I got the right answer so

                    Simulator simulator = new Simulator(map);
                    Console.WriteLine(simulator.TotalNewObstacleLoops());
                }
            }
        }
    }
}
