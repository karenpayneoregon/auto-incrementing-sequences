using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using IncrementSequenceDemos.Classes;
using W = ConsoleHelperLibrary.Classes.WindowUtility;

// ReSharper disable once CheckNamespace
namespace IncrementSequenceDemos;

partial class Program
{

    [ModuleInitializer]
    public static void Init()
    {
        // use to reset invoices back to original state
        //Operations.Reset();

        Console.Title = $"Code sample {Assembly.GetExecutingAssembly().GetName().Name!.SplitCamelCase()}";
        W.SetConsoleWindowPosition(W.AnchorWindow.Center);

        if (!File.Exists(Operations.FileName))
        {
            Operations.CreateReadInvoice();
        }

    }

    #region User interface 
    private static void Render(Rule rule)
    {
        AnsiConsole.Write(rule);
        AnsiConsole.WriteLine();
    }

    private static void ExitPrompt()
    {
        Console.WriteLine();
        Render(new Rule($"[yellow]Press a key to exit the demo[/]").RuleStyle(Style.Parse("silver")).Centered());
    }
    private static Table CreateViewTable() =>
        new Table()
            .Border(TableBorder.Square)
            .BorderColor(Color.Grey100)
            .Centered()
            .Title("[yellow][B]Invoices[/][/]")
            .AddColumn(new TableColumn("[u]Id[/]"))
            .AddColumn(new TableColumn("[u]Number[/]")); 
    #endregion
}