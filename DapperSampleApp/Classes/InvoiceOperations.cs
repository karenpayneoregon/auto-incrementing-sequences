using Microsoft.Data.SqlClient;
using System.Data;
using System.Transactions;
using Dapper;
using DapperSampleApp.Models;
using SequenceLibrary;
using static ConfigurationLibrary.Classes.ConfigurationHelper;

namespace DapperSampleApp.Classes;
internal class InvoiceOperations
{
    private IDbConnection _cn = new SqlConnection(ConnectionString());

    /// <summary>
    /// Set next value for sequence
    /// </summary>
    /// <param name="customers">Valid <see cref="Customers"/></param>
    public (bool success, Exception exception) SetSequence(Customers customers)
    {
        var sequenceValue = "";

        if (customers.CurrentSequenceValue is null)
        {
            // set default
            customers.CurrentSequenceValue = "000000";
            sequenceValue = customers.CurrentSequenceValue.NextCustomerInvoiceNumber();
        }
        else
        {
            sequenceValue = customers.CurrentSequenceValue.NextCustomerInvoiceNumber();
        }

        customers.CurrentSequenceValue = sequenceValue;

        const string statement =
            """
            UPDATE [dbo].[CustomerSequence]
              SET CurrentSequenceValue = @CurrentSequenceValue
            WHERE CustomerIdentifier = @CustomerIdentifier
            """;

        try
        {
            using TransactionScope transScope = new(TransactionScopeAsyncFlowOption.Enabled);
            _cn.Execute(statement, new
            {
                CurrentSequenceValue = sequenceValue,
                CustomerIdentifier = customers.CustomerIdentifier
            });
            transScope.Complete();
            return (true, null)!;
        }
        catch (TransactionAbortedException ex)
        {
            return (false, ex);
        }
    }

    /// <summary>
    /// Get all <see cref="Customers"/>
    /// </summary>
    /// <returns>Return all <see cref="Customers"/></returns>
    public List<Customers> CustomersList()
    {
        const string statement =
            """
            SELECT C.CustomerIdentifier,
                   C.CompanyName,
            	   CS.Id,
                   CS.SequencePreFix,
                   CS.CurrentSequenceValue
            FROM dbo.Customers AS C
                INNER JOIN dbo.CustomerSequence AS CS
                    ON C.CustomerIdentifier = CS.CustomerIdentifier;
            """;

        return _cn.Query<Customers>(statement).AsList();
    }

    /// <summary>
    /// Get sequence for a specific customer
    /// </summary>
    /// <param name="id">Customer primary key</param>
    /// <returns></returns>
    public CustomerSequence? Get(int id)
    {
        const string statement =
            """
            SELECT Id
                  ,CustomerIdentifier
                  ,CurrentSequenceValue
                  ,SequencePreFix
              FROM NextValueDatabase.dbo.CustomerSequence
            WHERE CustomerIdentifier = @CustomerIdentifier
            """;

        return _cn.QueryFirstOrDefault<CustomerSequence>(statement, new { CustomerIdentifier = id});
    }
}
