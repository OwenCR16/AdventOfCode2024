using System.Text;

namespace AdventOfCodeD6P2
{
    class Simulator
    {
        //CO-ORDINATES ARE [ X , Y ] (Y is negative as row index increases downwards)
        public Simulator(List<string> map)
        {
            Map = map;
            ObstacleLocations = GetObstacleLocations();
            InitialGuardLocation = GetGuardLocation();
            GuardLocation = InitialGuardLocation;
            AllNewObstacleLocations = GetNewObstacleLocations();
        }
        public List<string> Map { get; set; } = [];
        public List<int[]> ObstacleLocations { get; set; } = [];
        public List<int[]> AllNewObstacleLocations { get; set; } = [];
        public List<int[]> LocationsContainingMultipleDirectionalChanges { get; set; } = [];
        public List<List<char>> DirectionalChanges { get; set; } = [];
        public int[] InitialGuardLocation { get; set; }
        public int[] InitialGuardDirection { get; set; } = [0, -1];
        public int[] GuardLocation { get; set; }
        public int[] GuardDirection { get; set; } = [0, -1]; //default direciton is up
        public int[] NewObstacleLocation { get; set; } = [0, 0];
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
        public List<int[]> GetNewObstacleLocations()
        {
            List<int[]> newObstacleLocations = [];
            if (CheckSimulationCompletes())
            {
                for (int rowIndex = 0; rowIndex < Map.Count; rowIndex++)
                {
                    for (int collumnIndex = 0; collumnIndex < Map[rowIndex].Length; collumnIndex++)
                    {
                        if (Map[rowIndex][collumnIndex] != '#' && Map[rowIndex][collumnIndex] != '^' && Map[rowIndex][collumnIndex] != '.')
                            newObstacleLocations.Add([collumnIndex, rowIndex]);
                    }
                }
            }
            return newObstacleLocations;
        }
        public int TotalNewObstacleLoops()
        {

            int totalNewObstacleLoops = 0;
            foreach (int[] obstacleLocation in AllNewObstacleLocations)
            {
                NewObstacleLocation[0] = obstacleLocation[0];
                NewObstacleLocation[1] = obstacleLocation[1];
                if (!CheckSimulationCompletes())
                    totalNewObstacleLoops++;
            }
            return totalNewObstacleLoops;
        }
        public bool CheckSimulationCompletes()
        {
            ResetLocationsContainingMultipleDirectionalChanges();
            ResetMap();
            GuardLocation = InitialGuardLocation;
            GuardDirection = InitialGuardDirection;
            bool initialGuardLocationChanged = false;
            do
            {
                //validating new position and recording the direction on the location:
                if (!CheckDirectionAndLocationAreUnique())
                    return false;

                RecordLocationAndDirectionOnMap(ReturnCurrentDirectionAsChar());

                if (GuardLocation[0] + GuardDirection[0] >= Map[GuardLocation[1]].Length || GuardLocation[1] + GuardDirection[1] >= Map.Count || GuardLocation[0] + GuardDirection[0] < 0 || GuardLocation[1] + GuardDirection[1] < 0)
                    return true;

                //direction changing and re-positioning location:
                bool correctDirection = false;
                do
                {
                    //this is supposed to just make the process faster, but somehow it gives me fewer results... I am lost
                    if (!initialGuardLocationChanged && GuardLocation[0] + GuardDirection[0] == NewObstacleLocation[0] && GuardLocation[1] + GuardDirection[1] == NewObstacleLocation[1])
                    {
                        InitialGuardLocation = [GuardLocation[0], GuardLocation[1]];
                        
                    }

                    if (Map[GuardLocation[1] + GuardDirection[1]][GuardLocation[0] + GuardDirection[0]] == '#' || (GuardLocation[0] + GuardDirection[0] == NewObstacleLocation[0] && GuardLocation[1] + GuardDirection[1] == NewObstacleLocation[1]))
                        GuardDirection = ChangeGuardDirection();
                    else
                    {
                        correctDirection = true;
                        if (!initialGuardLocationChanged && GuardLocation[0] + GuardDirection[0] == NewObstacleLocation[0] && GuardLocation[1] + GuardDirection[1] == NewObstacleLocation[1])
                        InitialGuardDirection = [GuardDirection[0], GuardDirection[1]];
                        initialGuardLocationChanged = true;
                    }
                } while (!correctDirection);
                
                GuardLocation = [GuardLocation[0] + GuardDirection[0], GuardLocation[1] + GuardDirection[1]];
            } while (true);
        }
        public bool CheckDirectionAndLocationAreUnique()
        {
            //if current location contains an already-recorded direction
            //  if current location is present in the list of locations containing multiple directional changes, 
            //      if any of those directions match the current direction, 
            //          return false
            //          otherwise, add the new direction to that list associated with this location
            //  otherwise, if it matches the current direction, 
            //      return false
            //      otherwise, make this location a location containing multiple directional changes


            if (Map[GuardLocation[1]][GuardLocation[0]] != '.')
            {
                bool inLCMDC = false;
                for (int index = 0; index < LocationsContainingMultipleDirectionalChanges.Count; index++)
                {
                    if (LocationsContainingMultipleDirectionalChanges[index][0] == GuardLocation[0] && LocationsContainingMultipleDirectionalChanges[index][1] == GuardLocation[1])
                    {
                        inLCMDC = true;
                        foreach (char direction in DirectionalChanges[index])
                        {
                            if (direction == ReturnCurrentDirectionAsChar())
                                return false;
                        }
                        DirectionalChanges[index].Add(ReturnCurrentDirectionAsChar());
                    }
                }

                if (!inLCMDC && Map[GuardLocation[1]][GuardLocation[0]] == ReturnCurrentDirectionAsChar())
                    return false;
                else if (!inLCMDC)
                {
                    LocationsContainingMultipleDirectionalChanges.Add([GuardLocation[0], GuardLocation[1]]);
                    DirectionalChanges.Add([ReturnCurrentDirectionAsChar(), Map[GuardLocation[1]][GuardLocation[0]]]);
                }
            }
            return true;
        }
        public void ResetLocationsContainingMultipleDirectionalChanges()
        {
            LocationsContainingMultipleDirectionalChanges = [];
            DirectionalChanges = [];
        }
        public void ResetMap()
        {
            for (int rowIndex = 0; rowIndex < Map.Count; rowIndex++)
            {
                for (int collumnIndex = 0; collumnIndex < Map[rowIndex].Length; collumnIndex++)
                {
                    if (Map[rowIndex][collumnIndex] != '#' && Map[rowIndex][collumnIndex] != '.' && Map[rowIndex][collumnIndex] != '^')
                    {
                        StringBuilder sb = new StringBuilder(Map[rowIndex]);
                        sb[collumnIndex] = '.';
                        Map[rowIndex] = sb.ToString();
                    }
                }
            }
        }
        public char ReturnCurrentDirectionAsChar()
        {
            switch (GuardDirection)
            {
                case [0, -1]:
                    return 'U';
                case [1, 0]:
                    return 'R';
                case [0, 1]:
                    return 'D';
                case [-1, 0]:
                    return 'L';
            }
            return 'X';
        }
        public void RecordLocationAndDirectionOnMap(char direction)
        {
            StringBuilder sb = new StringBuilder(Map[GuardLocation[1]]);
            sb[GuardLocation[0]] = direction;
            Map[GuardLocation[1]] = sb.ToString();
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