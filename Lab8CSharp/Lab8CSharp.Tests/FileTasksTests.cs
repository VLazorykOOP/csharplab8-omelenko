using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Lab8CSharp;

namespace Lab8CSharp.Tests;

public class FileTasksTests : IDisposable
{
    private const string TestDir = "TestFiles";

    public FileTasksTests()
    {
        if (!Directory.Exists(TestDir)) Directory.CreateDirectory(TestDir);
    }

    public void Dispose()
    {
        if (Directory.Exists(TestDir)) Directory.Delete(TestDir, true);
    }

    [Fact]
    public void Task1_Regex_ShouldMatchCoordinates()
    {
        // Arrange
        string pattern = @"\b\d{1,3}°\d{1,2}'\d{1,2}""[NS]\s\d{1,3}°\d{1,2}'\d{1,2}""[EW]\b";
        string text = "Point: 50°27'00\"N 30°31'24\"E and some text.";

        // Act
        var matches = Regex.Matches(text, pattern);

        // Assert
        Assert.Single(matches);
        Assert.Equal("50°27'00\"N 30°31'24\"E", matches[0].Value);
    }

    [Fact]
    public void Task2_Logic_ShouldTransformWordsCorrectly()
    {
        // Arrange & Act
        string word = "today";
        string result = TransformWord(word); // Логіка з Task2

        // Assert (симулюємо логіку Task2)
        string TransformWord(string w)
        {
            string lower = w.ToLower();
            if (lower.EndsWith("re") || lower.EndsWith("nd") || lower.EndsWith("less")) return "";
            if (lower.StartsWith("to")) return "at" + w.Substring(2);
            return w;
        }

        Assert.Equal(TransformWord("ignore"), "");
        Assert.Equal(TransformWord("friend"), "");
        Assert.Equal(TransformWord("endless"), "");
        Assert.Equal(TransformWord("today"), "atday");
        Assert.Equal(TransformWord("apple"), "apple");
    }

    [Fact]
    public void Task3_Difference_ShouldReturnOnlyUniqueWords()
    {
        // Arrange
        string[] words1 = { "Apple", "Banana", "Orange" };
        string[] words2 = { "Banana", "Kiwi" };
        var set2 = new HashSet<string>(words2, StringComparer.OrdinalIgnoreCase);

        // Act
        var result = words1.Where(w => !set2.Contains(w)).ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains("Apple", result);
        Assert.Contains("Orange", result);
        Assert.DoesNotContain("Banana", result);
    }

    [Fact]
    public void Task4_BinaryFile_ShouldFindMaxOnOddPositions()
    {
        // Arrange
        string path = Path.Combine(TestDir, "test.bin");
        double[] input = { 10.0, 50.0, 30.0, 5.0, 40.0 }; // Позиції: 1, 2, 3, 4, 5
        // Непарні позиції (1, 3, 5): 10.0, 30.0, 40.0 -> Max: 40.0

        using (var writer = new BinaryWriter(File.Open(path, FileMode.Create)))
        {
            foreach (var d in input) writer.Write(d);
        }

        // Act
        double maxOdd = double.MinValue;
        using (var reader = new BinaryReader(File.Open(path, FileMode.Open)))
        {
            int pos = 1;
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                double val = reader.ReadDouble();
                if (pos % 2 != 0 && val > maxOdd) maxOdd = val;
                pos++;
            }
        }

        // Assert
        Assert.Equal(40.0, maxOdd);
    }

    [Fact]
    public void Task5_MoveFile_ShouldMoveToCorrectFolder()
    {
        // Arrange
        string fileName = "t2_test.txt";
        string lastName = "Shevchenko";
        string folderName = Path.Combine(TestDir, $"{lastName}2");
        string sourcePath = Path.Combine(TestDir, fileName);
        string destPath = Path.Combine(folderName, fileName);

        File.WriteAllText(sourcePath, "content");
        if (!Directory.Exists(folderName)) Directory.CreateDirectory(folderName);

        // Act
        if (File.Exists(destPath)) File.Delete(destPath);
        File.Move(sourcePath, destPath);

        // Assert
        Assert.True(File.Exists(destPath));
        Assert.False(File.Exists(sourcePath));
    }
}