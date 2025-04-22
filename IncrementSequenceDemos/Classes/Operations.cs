using System.Data;
using Dapper;
using IncrementSequenceDemos.Data;
using IncrementSequenceDemos.Models;
using Microsoft.Data.SqlClient;
using SequenceLibrary;
using static System.DateTime;
using static ConfigurationLibrary.Classes.ConfigurationHelper;

namespace IncrementSequenceDemos.Classes;

public class Operations
{
    /// <summary>
    /// Give the file holding information a name which prying eye's most likely
    /// will not look at.
    /// </summary>
    public static string FileName => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataLibrary.dll");


    public static void Reset()
    {
        File.Delete(FileName);
    }

    /// <summary>
    /// Create encrypted file with invoices
    /// </summary>
    public static void SerializeInvoices()
    {

        CryptoSerializer<Invoice> cryptoSerializer = new(Secrets.Key);
        using FileStream fileStream = new(FileName, FileMode.OpenOrCreate);
        cryptoSerializer.Serialize(Invoices, fileStream);
    }

    /// <summary>
    /// Used for updating/saving in <seealso cref="Save"/>
    /// </summary>
    /// <param name="list"></param>
    public static void SerializeInvoices(List<Invoice> list)
    {
        CryptoSerializer<Invoice> cryptoSerializer = new(Secrets.Key);
        using FileStream fileStream = new(FileName, FileMode.OpenOrCreate);
        cryptoSerializer.Serialize(list, fileStream);
    }

    /// <summary>
    /// Read invoices from encrypted file <seealso cref="FileName"/>
    /// </summary>
    /// <returns></returns>
    public static List<Invoice> DeserializeInvoices()
    {
        CryptoSerializer<Invoice> cryptoSerializer = new(Secrets.Key);
        using FileStream fileStream = new(FileName, FileMode.Open);
        return cryptoSerializer.Deserialize(fileStream);

    }

    /// <summary>
    /// For initial creation of the file
    /// </summary>
    public static List<Invoice> Invoices => new()
    {
        new() { Id = 1, Number = "F1124" },
        new() { Id = 2, Number = "1278-120"},
        new() { Id = 3, Number = "3999/IKL/VII/21"},
        new() { Id = 4, Number = "0001"},
        new() { Id = 5, Number = "AA0001"},
        new() { Id = 6, Number = "BB0200"},
        new() { Id = 7, Number = $"BB-{Now.Year}-{Now.Month:D2}-0200"},
        // id, register number, transaction date/time, transaction number
        new() { Id = 8, Number = $"KP,22,{Now:MM/dd/yyyy hh:mm},01"},
    };

    /// <summary>
    /// Save changes back to <seealso cref="FileName"/>
    /// </summary>
    /// <param name="list">list of <see cref="Invoices"/></param>
    /// <remarks>
    /// newValue is used so those who want to traverse code in the debugger this
    /// makes it easier.
    /// </remarks>
    public static void Save(List<Invoice> list)
    {
        for (int index = 0; index < list.Count; index++)
        {
            var newValue = Helpers.NextValue(list[index].Number);
            list[index].Number = newValue;
        }

        SerializeInvoices(list);
    }

    /// <summary>
    /// Create initial file
    /// </summary>
    public static void CreateReadInvoice()
    {
        SerializeInvoices();
    }

    /// <summary>
    /// Example to increment value by user supplied value
    /// </summary>
    public static void IncrementByValue()
    {
        var invoiceNumber = "AQW-23-10";
        Console.WriteLine(Helpers.NextValue(invoiceNumber,5));

        invoiceNumber = "AQW-23-10";
        Console.WriteLine(Helpers.NextValue(invoiceNumber, 10));

    }

    public static void TruncateExample1Table()
    {
        var connectionString = "Server=(localdb)\\mssqllocaldb;Database=NextValueDatabase;integrated security=True;Encrypt=True";
        using var cn = new SqlConnection(connectionString);
        using var cmd = new SqlCommand() { Connection = cn };

        cmd.CommandText = "TRUNCATE TABLE dbo.Example1";
        cn.Open();
        cmd.ExecuteNonQuery();
    }

    /// <summary>
    /// This example is no different from the other samples other than data is place
    /// into a SQL-Server database table, see the script under Solution Explorer script folder.
    /// </summary>
    public static void DataProviderExample()
    {
        int someValue = 0;
        int maxValue = 2022;

        using var cn = new SqlConnection(ConnectionString());
        using var cmd = new SqlCommand() {Connection = cn};

        cmd.CommandText = "TRUNCATE TABLE dbo.Example";
        cn.Open();
        cmd.ExecuteNonQuery();

        cmd.CommandText = "INSERT INTO dbo.Example (Value) VALUES (@Value)";
        cmd.Parameters.Add("@Value", SqlDbType.NVarChar);

        while (someValue < maxValue)
        {
            cmd.Parameters["@Value"].Value = $"A{Helpers.NextValue($"{someValue:D3}")}/{maxValue}";
            cmd.ExecuteNonQuery();
            someValue++;
        }
    }

    public static void DataProviderExample1()
    {
        int someValue = 0;
        int maxValue = 2022;

        using var cn = new SqlConnection(ConnectionString());
        cn.Open();

        // Truncate the table first
        cn.Execute("TRUNCATE TABLE dbo.Example");

        // Prepare the insert SQL
        const string insertSql = "INSERT INTO dbo.Example (Value) VALUES (@Value)";

        while (someValue < maxValue)
        {
            var value = $"A{Helpers.NextValue($"{someValue:D3}")}/{maxValue}";
            cn.Execute(insertSql, new { Value = value });
            someValue++;
        }
    }


    public static void EntityFrameworkExample1()
    {
        TruncateExample1Table();

        int someValue = 0;
        int maxValue = 20;
        using var context = new Context();

        while (someValue < maxValue)
        {
            context.Add(new Example1() { InvoiceNumber = $"A{Helpers.NextInvoiceNumber($"{someValue:D3}")}/{maxValue}" });
            someValue++;
        }

        context.SaveChanges();
    }

    public static bool EntityFrameworkExample2(int customerId)
    {
        using var context = new Context();
        // Get customer to add a new order
        var customer = context.CustomerSequence.FirstOrDefault(x => x.CustomerIdentifier == customerId);
        if (customer is not null)
        {
            var prefix = customer.SequencePreFix;
            var sequenceValue = customer.CurrentSequenceValue;

            /*
             * If this is the first order for a customer start the sequence, otherwise increment
             * the sequence
             */
            sequenceValue = string.IsNullOrWhiteSpace(sequenceValue) ? 
                $"{prefix}{Helpers.NextValue("0")}" : 
                Helpers.NextValue(sequenceValue);

            // update the sequence
            customer.CurrentSequenceValue = sequenceValue;

            // add a new order
            Orders order = new() { CustomerIdentifier = customer.Id, InvoiceNumber = sequenceValue, OrderDate = DateTime.Now };

            context.Orders.Add(order);
            return context.SaveChanges() == 2;


        }
        else
        {
            return false;
        }
    }
}