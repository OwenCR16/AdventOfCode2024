namespace AdventOfCodeDay5P2
{
    class Update
    {
        public Update(int[] pages)
        {
            Pages = pages;
        }
        public bool Correct { get; set; } = false;
        public int[] Pages { get; set; }
        public int MiddlePageNumber { get; set; }
    }
}