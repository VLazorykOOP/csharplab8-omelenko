using System;
using System.Collections.Generic;
using System.Text;

namespace Lab8CSharp;

public class Task5
{
    public static void Run(string lastName)
    {
        string folderName = $"{lastName}2";
        string fileName = "t2.txt";

        if (!Directory.Exists(folderName))
            Directory.CreateDirectory(folderName);

        if (File.Exists(fileName))
        {
            string destFile = Path.Combine(folderName, fileName);
            if (File.Exists(destFile)) File.Delete(destFile);

            File.Move(fileName, destFile);
            Console.WriteLine($"Файл перенесено в {folderName}");
        }
        else
        {
            Console.WriteLine("Файл t2.txt не знайдено.");
        }
    }
}
