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
    if (SurveyLevels(report))
        return true;
    else
    {
        for (int i = 0; i < report.Count; i++)
        {
            if (SurveyLevels(report, i))
                return true;
        }
        return false;
    }
}

bool SurveyLevels(List<int> report, int skipIndex = -1)
{
    bool increasingTrue = CheckGradient(report, skipIndex);
    int previousLevel = 0;
    int levelCount = 0;
    int levelCountForSkip = 0;
    foreach (int level in report)
    {
        levelCount++;
        levelCountForSkip++;
        if (levelCountForSkip - 1 == skipIndex)
        {
            levelCount--;
            continue;
        }
        if (levelCount > 1 && increasingTrue && previousLevel > level)
            return false;
        else if (levelCount > 1 && !increasingTrue && previousLevel < level)
            return false;
        else if (levelCount > 1 && (previousLevel == level || Math.Abs(previousLevel - level) > 3))
            return false;
        previousLevel = level;
    }
    return true;
}

bool CheckGradient(List<int> report, int skipIndex = -1)
{
    int previousLevel = 0;
    int levelCount = 0;
    int increases = 0;
    int decreases = 0;
    foreach (int level in report)
    {
        levelCount++;
        if (levelCount - 1 == skipIndex)
            continue;
        if (levelCount > 1 && previousLevel < level)
            increases++;
        else
            decreases++;
        previousLevel = level;
    }
    if (increases > decreases)
        return true;
    else
        return false;
}






