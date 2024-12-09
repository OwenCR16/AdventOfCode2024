namespace AdventOfCodeD4P1
{
    class XMAS
    {
        public XMAS(int initialCharIndex, int initialLineIndex)
        {
            InitialCharIndex = initialCharIndex;
            InitialLineIndex = initialLineIndex;
        }
        public int InitialCharIndex { get; set; } = 0;
        public int InitialLineIndex {get; set; } = 0;
        public int[] Gradient { get; set; } = [0, 1];
    }
}