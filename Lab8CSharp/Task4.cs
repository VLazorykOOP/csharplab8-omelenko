using System;
using System.Collections.Generic;
using System.Text;

namespace Lab8CSharp;

public class Task4
{
    public static void Run()
    {
        double[] numbers = { 1.5, 10.2, 3.4, 50.8, 4.1, 5.5 };
        string fileName = "data.bin";

        using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create)))
        {
            foreach (double n in numbers) writer.Write(n);
        }

        double maxOddPos = double.MinValue;
        using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
        {
            int index = 1;
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                double val = reader.ReadDouble();
                if (index % 2 != 0)
                {
                    if (val > maxOddPos) maxOddPos = val;
                }
                index++;
            }
        }
        Console.WriteLine($"Максимум на непарних позицiях: {maxOddPos}");
    }
}
