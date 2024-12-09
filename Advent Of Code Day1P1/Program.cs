List<int> officeGroupIDsList = new List<int>();
List<int> ownListGroupIDsList = new List<int>();

//I looked up help for the while loop as dealing with large amounts of data was new to me
while (Console.ReadLine() is string line)
{
    if (!string.IsNullOrEmpty(line))
    {
        var allIDs = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (int.TryParse(allIDs[0], out int number1))
        {
            officeGroupIDsList.Add(number1);
        }
        if (int.TryParse(allIDs[1], out int number2))
        {
            ownListGroupIDsList.Add(number2);
        }
    }
    if (string.IsNullOrEmpty(line))
    {
        Console.WriteLine(SumDistances(officeGroupIDsList, ownListGroupIDsList));
    }
}

int SumDistances(List<int> officeGroupIDsList, List<int> ownListGroupIDsList)
{
    officeGroupIDsList.Sort();
    ownListGroupIDsList.Sort();
    int sumOfDistances = 0;
    for (int i = 0; i < officeGroupIDsList.Count; i++)
    {
        sumOfDistances += Math.Abs(officeGroupIDsList[i] - ownListGroupIDsList[i]);
    }
    return sumOfDistances;
}






