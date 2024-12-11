namespace AdventOfCodeDay5P2
{
    class Calculator
    {
        public List<Update> FindIncorrectUpdates(PagePairRules pagePairRules, List<Update> updates)
        {
            List<Update> incorrectUpdates = [];
            foreach (Update update in updates)
            {
                if (!(update.Correct = pagePairRules.IdentifyCorrectUpdate(update)))
                    incorrectUpdates.Add(new Update(update.Pages));
            }
            return incorrectUpdates;
        }
        public int[] OrderUpdate(PagePairRules pagePairRules, Update update)
        {
            int[] correctedUpdatePages = update.Pages;
            bool corrected = false;
            do
            {
                int[] incorrectIndexes = pagePairRules.IdentifyIndexesOfIncorrectPages(correctedUpdatePages);
                if (incorrectIndexes[0] == -1 && incorrectIndexes[1] == -1)
                {
                    corrected = true;
                    continue;
                }
                int temp = correctedUpdatePages[incorrectIndexes[0]];
                correctedUpdatePages[incorrectIndexes[0]] = correctedUpdatePages[incorrectIndexes[1]];
                correctedUpdatePages[incorrectIndexes[1]] = temp;
            } while (!corrected);
            return correctedUpdatePages;
        }
        public List<Update> FixIncorrectUpdates(PagePairRules pagePairRules, List<Update> incorrectUpdates)
        {
            List<Update> correctedUpdates = [];

            foreach (Update update in incorrectUpdates)
            {
                correctedUpdates.Add(new Update(OrderUpdate(pagePairRules, update)));
            }
            return correctedUpdates;
        }
        public int FixIncorrectUpdatesAndSumMiddlePages(PagePairRules pagePairRules, List<Update> updates)
        {
            int result = 0;
            List<Update> correctedUpdates = FixIncorrectUpdates(pagePairRules, FindIncorrectUpdates(pagePairRules, updates));
            foreach (Update update in correctedUpdates)
            {
                result += update.Pages[(update.Pages.Length - 1) / 2];
            }
            return result;
        }
    }
}