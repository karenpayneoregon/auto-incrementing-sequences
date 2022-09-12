﻿using System.Data;
using IncrementSequenceDemos.Models;
using Microsoft.Data.SqlClient;
using SequenceLibrary;
using static System.DateTime;


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

    /// <summary>
    /// This example is no different from the other samples other than data is place
    /// into a SQL-Server database table, see the script under Solution Explorer script folder.
    /// </summary>
    public static void DataExample()
    {
        var connectionString = 
            "Server=(localdb)\\mssqllocaldb;Database=NextValueDatabase;integrated security=True;Encrypt=True";
        int someValue = 0;
        int maxValue = 2022;

        using var cn = new SqlConnection(connectionString);
        using var cmd = new SqlCommand() {Connection = cn};
        cmd.CommandText = "INSERT INTO dbo.Example (Value) VALUES (@Value)";
        cmd.Parameters.Add("@Value", SqlDbType.NVarChar);
        cn.Open();

        while (someValue < maxValue)
        {
            cmd.Parameters["@Value"].Value = $"{Helpers.NextValue($"{someValue:D3}")}/{maxValue}";
            cmd.ExecuteNonQuery();
            someValue++;
        }
    }
}