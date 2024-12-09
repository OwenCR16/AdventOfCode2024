namespace AdventOfCodeD4P2
{
    class Counter
    {
        public int CountCrossMasAppearances(List<string> lines)
        {
            int lineIndex = 0;
            List<MAS> allMAS = [];
            foreach (string line in lines)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == 'M')
                    {

                        MAS mAS = new MAS(i, lineIndex);
                        SearchBounds searchBounds = new SearchBounds();
                        searchBounds.InitialLineIndex = lineIndex == 0 ? 0 : -1;
                        searchBounds.FinalLineIndexPlusOne = lineIndex == lines.Count - 1 ? 1 : 2;
                        searchBounds.InitialCharIndex = i == 0 ? 0 : -1;
                        searchBounds.FinalCharIndexPlusOne = i == line.Length - 1 ? 1 : 2;
                        foreach (MAS mASResult in allMASFromLetter(mAS, searchBounds, lines))
                        {
                            allMAS.Add(mASResult);
                        }
                    }
                }
                lineIndex++;
            }
            return totalNumberOfCrossedMAS(allMAS);
        }
        public int totalNumberOfCrossedMAS(List<MAS> allMAS)
        {
            int total = 0;
            foreach (MAS mASOne in allMAS)
            {
                foreach (MAS mASTwo in allMAS)
                {
                    if (mASOne.LetterAPosition[0] == mASTwo.LetterAPosition[0] && mASOne.LetterAPosition[1] == mASTwo.LetterAPosition[1])
                    {
                        if (CheckGradientsCross(mASOne.Gradient, mASTwo.Gradient)) //to filter out comparing the same MAS with itself
                            total++;
                    }
                }
            }
            return total / 2; //every cross will be found twice
        }
        public bool CheckGradientsCross(int[] gradientOne, int[] gradientTwo)
        {
            if ((gradientOne[0] == -gradientTwo[0] && gradientOne[1] == gradientTwo[1]) || (gradientOne[0] == gradientTwo[0] && gradientOne[1] == -gradientTwo[1]))
                return true;
            return false;
        }
        public List<MAS> allMASFromLetter(MAS mAS, SearchBounds searchBounds, List<string> lines)
        {
            List<MAS> allMAS = [];
            Letter letterOne = new Letter('M', 'A', mAS.InitialCharIndex, mAS.InitialLineIndex);
            List<int[]> gradients = FindGradients(letterOne, searchBounds, lines);
            foreach (int[] gradient in gradients)
            {
                mAS.Gradient = gradient;
                mAS.LetterAPosition = [mAS.InitialCharIndex + mAS.Gradient[0], mAS.InitialLineIndex + mAS.Gradient[1]];
                Letter letterTwo = new Letter('A', 'S', mAS.InitialCharIndex + mAS.Gradient[0], mAS.InitialLineIndex + mAS.Gradient[1]);
                if (!FilterEndOfBounds(letterTwo, mAS, lines))
                {
                    if (TryFindAdjacentLetter(letterTwo, mAS, lines))
                        allMAS.Add(new MAS(mAS.InitialCharIndex, mAS.InitialLineIndex) { Gradient = mAS.Gradient, LetterAPosition = mAS.LetterAPosition });
                }
            }
            return allMAS;
        }
        public List<int[]> FindGradients(Letter letter, SearchBounds searchBounds, List<string> lines)
        {
            List<int[]> gradients = [];
            int[] gradient = [0, 0]; //[x axis, y axis]
            for (int j = searchBounds.InitialLineIndex; j < searchBounds.FinalLineIndexPlusOne; j++)
            {
                for (int k = searchBounds.InitialCharIndex; k < searchBounds.FinalCharIndexPlusOne; k++)
                {
                    gradient = [k, j];
                    if (lines[letter.LineIndex + j][letter.CharIndex + k] == letter.NextDesiredChar && gradient[0] != 0 && gradient[1] != 0) //only diagonals
                        gradients.Add(gradient);
                }
            }
            return gradients;
        }
        public bool TryFindAdjacentLetter(Letter letter, MAS mAS, List<string> lines)
        {
            if (lines[letter.LineIndex + mAS.Gradient[1]][letter.CharIndex + mAS.Gradient[0]] == letter.NextDesiredChar)
                return true;
            return false;
        }
        public bool FilterEndOfBounds(Letter letter, MAS mAS, List<string> lines)
        {
            if ((letter.CharIndex == lines[letter.LineIndex].Length - 1 && mAS.Gradient[0] == 1) || (letter.CharIndex == 0 && mAS.Gradient[0] == -1))
                return true;
            else if ((letter.LineIndex == lines.Count - 1 && mAS.Gradient[1] == 1) || (letter.LineIndex == 0 && mAS.Gradient[1] == -1))
                return true;
            return false;
        }
    }
}