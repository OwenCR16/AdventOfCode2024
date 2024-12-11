namespace AdventOfCodeDay5P1
{
    class PagePairRules
    {
        public List<int[]> AllRules { get; set; } = [];
        public bool IdentifyCorrectUpdate(Update update)
        {
            foreach (int[] rule in AllRules)
            {
                int IndexOne = -1;
                int IndexTwo = -1;
                //last index of the second, first index of the first.
                if ((IndexOne = Array.IndexOf(update.Pages, rule[0])) != -1 && (IndexTwo = Array.LastIndexOf(update.Pages, rule[1])) != -1)
                {
                    if (IndexOne > IndexTwo)
                        return false;
                }
            }
            return true;
        }
    }
}