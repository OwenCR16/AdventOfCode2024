List<List<int>> reports = new List<List<int>>();

while (Console.ReadLine() is string line)
{
    if (!string.IsNullOrEmpty(line))
    {
        List<int> report = new List<int>();
        var levels = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        foreach (var level in levels)
        {
            if (int.TryParse(level, out int levelInt))
                report.Add(levelInt);
        }
        reports.Add(report);
    }
    if (string.IsNullOrEmpty(line))
    {
        Console.WriteLine(SumSafeReports(reports));
    }
}

int SumSafeReports(List<List<int>> reports)
{
    int safeReportTotal = 0;
    foreach (List<int> report in reports)
    {
        if (CheckSafeReport(report))
            safeReportTotal++;
    }
    return safeReportTotal;
}

bool CheckSafeReport(List<int> report)
{
    int previousLevel = 0;
    int levelCount = 0;
    bool increasingTrue = false;
    foreach (int level in report)
        {
            levelCount++;
            if (levelCount == 2 && previousLevel < level)
                increasingTrue = true;
            if (levelCount > 2 && increasingTrue && previousLevel > level)
                return false;
            if (levelCount > 2 && !increasingTrue && previousLevel < level)
                return false;
            if (levelCount > 1 && (previousLevel == level || Math.Abs(previousLevel - level) > 3))
                return false;
            previousLevel = level;
        }
    return true;
}





