using System;
using System.Collections.Generic;
using System.Text;

namespace Lab8CSharp;

public class Task3
{
    public static void Run()
    {
        char[] separators = { ' ', ',', '.', '!', '?', ';', ':', '-', '\n', '\r' };

        string text1 = File.ReadAllText("text1.txt");
        string text2 = File.ReadAllText("text2.txt");

        var words2 = new HashSet<string>(text2.Split(separators, StringSplitOptions.RemoveEmptyEntries), StringComparer.OrdinalIgnoreCase);
        var words1 = text1.Split(separators, StringSplitOptions.RemoveEmptyEntries);

        List<string> result = new List<string>();
        foreach (var w in words1)
        {
            if (!words2.Contains(w)) result.Add(w);
        }

        File.WriteAllText("output3.txt", string.Join(" ", result));
    }
}
