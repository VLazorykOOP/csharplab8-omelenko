namespace Lab8CSharp;

class Program
{
    static void Main()
    {
        Console.WriteLine("Виберiть номер завдання (1-5):");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1": Task1.Run(); break;
            case "2": Task2.Run(); break;
            case "3": Task3.Run(); break;
            case "4": Task4.Run(); break;
            case "5":
                Console.WriteLine("Введiть прiзвище:");
                Task5.Run(Console.ReadLine());
                break;
        }
    }
}