using System.Text;
using IncrementSequenceDemos.Classes;
using IncrementSequenceDemos.Models;
using SequenceLibrary;

namespace IncrementSequenceDemos
{
    partial  class Program
    {
        static void Main(string[] args)
        {
            //Operations.DapperDataProviderExample();

            //Operations.EntityFrameworkExample1();
            var success = Operations.EntityFrameworkExample2(3);
            if (success)
            {
                AnsiConsole.MarkupLine("[cyan]Order added[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[cyan]Order not added[/]");
            }

            //InvoiceExample();
            AnsiConsole.MarkupLine("[white on blue]Done[/]");
            Console.ReadLine();
        }

        private static void InvoiceExample()
        {

            var table = CreateViewTable();

            List<Invoice> list = Operations.DeserializeInvoices();

            foreach (var invoice in list)
            {
                table.AddRow(invoice.Id.ToString(), invoice.Number);
            }

            for (int index = 0; index < 7; index++)
            {
                Console.WriteLine();
            }

            AnsiConsole.Write(table);

            Operations.Save(list);


            ExitPrompt();

        }

        private static void TransactionDemo()
        {
            var item = $"KP,22,{DateTime.Now:MM/dd/yyyy hh:mm},01";
            var parts = item.Split(",");

            foreach (var part in parts)
            {
                Console.WriteLine(part);
            }
        }

        private static void SimpleStaticExample()
        {
            var data = Enumerable.Range(1, 10020);
            StringBuilder builder = new();
            foreach (var arrayInt in data)
            {
                builder.AppendLine(Helpers.NextValueFromIdentifier('x', arrayInt));
            }

            Console.WriteLine(builder);
            ExitPrompt();

        }
    }
}