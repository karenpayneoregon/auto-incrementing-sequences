using KP_ConsoleAppNet71.Classes;

namespace KP_ConsoleAppNet71;

internal partial class Program
{

    static void Main(string[] args)
    {
        AnsiConsole.MarkupLine("[yellow]Hello[/]");
        Console.WriteLine(StringHelpers.IncrementLetter("a"));
        Console.WriteLine(StringHelpers.IncrementLetter("ZZ"));
        Console.ReadLine();
    }


}

