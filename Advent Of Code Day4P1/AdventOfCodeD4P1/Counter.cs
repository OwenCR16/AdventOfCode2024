namespace AdventOfCodeD4P1
{
    class Counter
    {
        public int CountXmasAppearances(List<string> lines)
        {
            int result = 0;
            int lineIndex = 0;
            foreach (string line in lines)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == 'X')
                    {

                        XMAS xMAS = new XMAS(i, lineIndex);
                        SearchBounds searchBounds = new SearchBounds();
                        searchBounds.InitialLineIndex = lineIndex == 0 ? 0 : -1;
                        searchBounds.FinalLineIndexPlusOne = lineIndex == lines.Count - 1 ? 1 : 2;
                        searchBounds.InitialCharIndex = i == 0 ? 0 : -1;
                        searchBounds.FinalCharIndexPlusOne = i == line.Length - 1 ? 1 : 2;
                        result += allXMASFromLetter(xMAS, searchBounds, lines);
                    }
                }
                lineIndex++;
            }
            return result;
        }
        public int allXMASFromLetter(XMAS xMAS, SearchBounds searchBounds, List<string> lines)
        {
            int xMASCount = 0;
            Letter letterOne = new Letter('X', 'M', xMAS.InitialCharIndex, xMAS.InitialLineIndex);
            List<int[]> gradients = FindGradients(letterOne, xMAS, searchBounds, lines);
            foreach (int[] gradient in gradients)
            {
                xMAS.Gradient = gradient;
                Letter letterTwo = new Letter('M', 'A', xMAS.InitialCharIndex + xMAS.Gradient[0], xMAS.InitialLineIndex + xMAS.Gradient[1]);
                if (!FilterEndOfBounds(letterTwo, xMAS, lines))
                {
                    if (TryFindAdjacentLetter(letterTwo, xMAS, lines))
                    {
                        Letter letterThree = new Letter('A', 'S', xMAS.InitialCharIndex + (xMAS.Gradient[0] * 2), xMAS.InitialLineIndex + (xMAS.Gradient[1] * 2));
                        if (!FilterEndOfBounds(letterThree, xMAS, lines))
                        {
                            if (TryFindAdjacentLetter(letterThree, xMAS, lines))
                            {
                                xMASCount++;
                            }
                        }
                    }
                }
            }
            return xMASCount;
        }
        public List<int[]> FindGradients(Letter letter, XMAS xMAS, SearchBounds searchBounds, List<string> lines)
        {
            List<int[]> gradients = [];
            int[] gradient = [0, 0]; //[x axis, y axis]
            for (int j = searchBounds.InitialLineIndex; j < searchBounds.FinalLineIndexPlusOne; j++)
            {
                for (int k = searchBounds.InitialCharIndex; k < searchBounds.FinalCharIndexPlusOne; k++)
                {
                    gradient = [k, j];
                    if (lines[letter.LineIndex + j][letter.CharIndex + k] == letter.NextDesiredChar)
                        gradients.Add(gradient);
                }
            }
            return gradients;
        }
        public bool TryFindAdjacentLetter(Letter letter, XMAS xMAS, List<string> lines)
        {
            if (lines[letter.LineIndex + xMAS.Gradient[1]][letter.CharIndex + xMAS.Gradient[0]] == letter.NextDesiredChar)
                return true;
            return false;
        }
        public bool FilterEndOfBounds(Letter letter, XMAS xMAS, List<string> lines)
        {
            if ((letter.CharIndex == lines[letter.LineIndex].Length - 1 && xMAS.Gradient[0] == 1) || (letter.CharIndex == 0 && xMAS.Gradient[0] == -1))
                return true;
            else if ((letter.LineIndex == lines.Count - 1 && xMAS.Gradient[1] == 1) || (letter.LineIndex == 0 && xMAS.Gradient[1] == -1))
                return true;
            return false;
        }
    }
}