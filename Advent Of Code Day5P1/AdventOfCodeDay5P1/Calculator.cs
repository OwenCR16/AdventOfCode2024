namespace AdventOfCodeDay5P1
{
    class Calculator
    {
        public int SumMiddlePageNumbersOfCorrectUpdates(PagePairRules pagePairRules, List<Update> updates)
        {
            int result = 0;
            foreach (Update update in updates)
            {
                //update.Correct = pagePairRules.IdentifyCorrectUpdate(update);
                if (update.Correct = pagePairRules.IdentifyCorrectUpdate(update))
                    result += update.Pages[(update.Pages.Length - 1) / 2];
            }
            return result;
        }
    }
}