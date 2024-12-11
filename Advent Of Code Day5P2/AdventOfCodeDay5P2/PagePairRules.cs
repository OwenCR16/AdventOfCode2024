namespace AdventOfCodeDay5P2
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
                if ((IndexOne = Array.FindIndex(update.Pages, page => page == rule[0])) != -1 && (IndexTwo = Array.LastIndexOf(update.Pages, rule[1])) != -1)
                {
                    if (IndexOne > IndexTwo)
                        return false;
                }
            }
            return true;
        }
        public int[] IdentifyIndexesOfIncorrectPages(int[] pages)
        {
            foreach (int[] rule in AllRules)
            {
                int IndexOne = -1;
                int IndexTwo = -1;
                if ((IndexOne = Array.FindIndex(pages, page => page == rule[0])) != -1 && (IndexTwo = Array.LastIndexOf(pages, rule[1])) != -1)
                {
                    if (IndexOne > IndexTwo)
                        return [IndexOne, IndexTwo];
                }
            }
            return [-1, -1];
        }
    }
}