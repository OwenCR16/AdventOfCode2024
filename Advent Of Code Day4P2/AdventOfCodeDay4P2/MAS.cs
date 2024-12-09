namespace AdventOfCodeD4P2
{
    class MAS
    {
        public MAS(int initialCharIndex, int initialLineIndex)
        {
            InitialCharIndex = initialCharIndex;
            InitialLineIndex = initialLineIndex;
        }
        public int InitialCharIndex { get; set; } = 0;
        public int InitialLineIndex { get; set; } = 0;
        public int[] Gradient { get; set; } = [0, 1];
        public int[] LetterAPosition { get; set; } = [0, 0];
    }
}