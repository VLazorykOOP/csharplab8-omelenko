using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab8CSharp;

public class Task2
{
    public static void Run()
    {
        string text = File.ReadAllText("input2.txt");

        string[] words = Regex.Split(text, @"(\W+)");

        for (int i = 0; i < words.Length; i++)
        {
            if (Regex.IsMatch(words[i], @"^[a-zA-Z]+$"))
            {
                string w = words[i].ToLower();

                if (w.EndsWith("re") || w.EndsWith("nd") || w.EndsWith("less"))
                {
                    words[i] = "";
                }
                else if (w.StartsWith("to"))
                {
                    words[i] = "at" + words[i].Substring(2);
                }
            }
        }

        File.WriteAllText("output2.txt", string.Join("", words));
    }
}
