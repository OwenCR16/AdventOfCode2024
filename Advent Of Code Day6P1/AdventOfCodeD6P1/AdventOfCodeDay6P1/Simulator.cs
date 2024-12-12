using System.Text;

namespace AdventOfCodeD6P1
{
    class Simulator
    {
        //CO-ORDINATES ARE [ X , Y ] (Y is negative as row index increases downwards)
        public Simulator(List<string> map)
        {
            Map = map;
            ObstacleLocations = GetObstacleLocations();
            GuardLocation = GetGuardLocation();
        }
        public List<string> Map { get; set; } = [];
        public List<int[]> ObstacleLocations { get; set; } = [];
        public int[] GuardLocation { get; set; }
        public int[] GuardDirection { get; set; } = [0, -1]; //default direciton is up
        public List<int[]> GetObstacleLocations()
        {
            List<int[]> obstacleLocations = [];
            for (int rowIndex = 0; rowIndex < Map.Count; rowIndex++)
            {
                for (int collumnIndex = 0; collumnIndex < Map[rowIndex].Length; collumnIndex++)
                {
                    if (Map[rowIndex][collumnIndex] == '#')
                        obstacleLocations.Add([collumnIndex, rowIndex]);
                }
            }
            return obstacleLocations;
        }
        public int[] GetGuardLocation()
        {
            int[] guardLocation = [];
            for (int rowIndex = 0; rowIndex < Map.Count; rowIndex++)
            {
                for (int collumnIndex = 0; collumnIndex < Map[rowIndex].Length; collumnIndex++)
                {
                    if (Map[rowIndex][collumnIndex] == '^')
                    {
                        guardLocation = [collumnIndex, rowIndex];
                        break;
                    }
                }
            }
            return guardLocation;
        }
        public int SumDistinctPositions()
        {
            int distinctPositions = 0;
            foreach (string row in Map)
            {
                distinctPositions += row.Count(c => c == 'X');
            }
            return distinctPositions;
        }
        public void RunSimulation()
        {
            do
            {
                StringBuilder sb = new StringBuilder(Map[GuardLocation[1]]);
                sb[GuardLocation[0]] = 'X';
                Map[GuardLocation[1]] = sb.ToString();

                if (GuardLocation[0] + GuardDirection[0] >= Map[GuardLocation[1]].Length || GuardLocation[1] + GuardDirection[1] >= Map.Count)
                    break;

                bool correctDirection = false;
                do
                {
                    if (Map[GuardLocation[1] + GuardDirection[1]][GuardLocation[0] + GuardDirection[0]] == '#')
                        GuardDirection = ChangeGuardDirection();
                    else
                        correctDirection = true;
                } while (!correctDirection);
                
                GuardLocation = [GuardLocation[0] + GuardDirection[0], GuardLocation[1] + GuardDirection[1]];
            } while (true);
        }
        public int[] ChangeGuardDirection()
        {
            int[] tempGuardDirection = [0, 0];
            switch (GuardDirection[0])
            {
                case -1:
                case 1:
                    tempGuardDirection[0] = 0;
                    break;
                case 0:
                    tempGuardDirection[0] = -GuardDirection[1];
                    break;
            }
            switch (GuardDirection[1])
            {
                case -1:
                case 1:
                    tempGuardDirection[1] = 0;
                    break;
                case 0:
                    tempGuardDirection[1] = GuardDirection[0];
                    break;
            }
            return tempGuardDirection;
        }
    }
}