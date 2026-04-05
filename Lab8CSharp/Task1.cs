using System.Text.RegularExpressions;

namespace Lab8CSharp;

public class Task1
{
    public static void Run()
    {
        string inputPath = "input1.txt";
        string outputPath = "coords.txt";
        
        // формат: 50°27'00"N 30°31'24"E
        string pattern = @"\b\d{1,3}°\d{1,2}'\d{1,2}""[NS]\s\d{1,3}°\d{1,2}'\d{1,2}""[EW]\b";

        string text = File.ReadAllText(inputPath);
        MatchCollection matches = Regex.Matches(text, pattern);

        List<string> foundCoords = new List<string>();
        foreach (Match m in matches) foundCoords.Add(m.Value);

        File.WriteAllLines(outputPath, foundCoords);
        Console.WriteLine($"Знайдено координат: {matches.Count}");

        Console.WriteLine("Введiть текст для замiни знайдених координат:");
        string replacement = Console.ReadLine();
        string newText = Regex.Replace(text, pattern, replacement);

        File.WriteAllText("processed_text1.txt", newText);
    }
}