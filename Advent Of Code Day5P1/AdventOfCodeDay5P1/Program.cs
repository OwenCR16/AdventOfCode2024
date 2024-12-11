namespace AdventOfCodeDay5P1
{
    class Program
    {
        static void Main(string[] args)
        {
            PagePairRules pagePairRules = new PagePairRules();
            List<Update> updates = [];
            while (Console.ReadLine() is string line)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    if (line.Contains('|'))
                    {
                        pagePairRules.AllRules.Add(line.Split('|', StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToArray());
                    }
                    else if (line.Contains(','))
                    {
                        updates.Add(new Update(line.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToArray()));
                    }
                }
                if (line == "crimus")
                {
                    Calculator calculator = new Calculator();
                    Console.WriteLine(calculator.SumMiddlePageNumbersOfCorrectUpdates(pagePairRules, updates));
                }
                //result is currently too low
            }
        }
    }
}


