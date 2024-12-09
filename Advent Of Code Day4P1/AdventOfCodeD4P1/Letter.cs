namespace AdventOfCodeD4P1
{
    class Letter
    {
        public Letter(char character, char nextDesiredChar, int charIndex, int lineIndex)
        {
            Char = character;
            NextDesiredChar = nextDesiredChar;
            LineIndex = lineIndex;
            CharIndex = charIndex;
        }
        public char Char { get; set; }
        public char NextDesiredChar { get; set; }
        public int LineIndex { get; set; }
        public int CharIndex { get; set; }
    }
}