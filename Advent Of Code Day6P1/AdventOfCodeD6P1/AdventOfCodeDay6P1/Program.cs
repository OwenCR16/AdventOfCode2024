namespace AdventOfCodeD6P1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> map = [];
            while (Console.ReadLine() is string row)
            {
                if (!string.IsNullOrEmpty(row))
                {
                    map.Add(row);
                }
                if (row == "crimus")
                {
                    Simulator simulator = new Simulator(map);
                    simulator.RunSimulation();

                    //prints the path
                    foreach (string newRow in simulator.Map)
                    {
                        Console.WriteLine(newRow);
                    }

                    //prints the answer
                    Console.WriteLine(simulator.SumDistinctPositions());
                }
            }
        }
    }
}