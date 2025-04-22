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
    /// Updates the sequence value for the specified customer.
    /// </summary>
    /// <param name="customers">An instance of <see cref="Customers"/> representing the customer whose sequence value is to be updated.</param>
    /// <returns>
    /// A tuple containing a boolean indicating success and an <see cref="Exception"/> if an error occurs.
    /// </returns>
    /// <remarks>
    /// This method updates the customer's sequence value in the database. If the current sequence value is null, 
    /// it initializes it with a default value before incrementing.
    /// </remarks>
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
    /// Retrieves a list of all customers along with their sequence information.
    /// </summary>
    /// <returns>
    /// A list of <see cref="Customers"/> objects, each containing customer details 
    /// and associated sequence information.
    /// </returns>
    /// <remarks>
    /// This method executes a SQL query to fetch customer data and their corresponding 
    /// sequence details by joining the Customers and CustomerSequence tables.
    /// </remarks>
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
    /// Retrieves the sequence information for a specific customer based on their identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the customer.</param>
    /// <returns>
    /// An instance of <see cref="CustomerSequence"/> containing the sequence details for the specified customer,
    /// or <c>null</c> if no matching record is found.
    /// </returns>
    /// <remarks>
    /// This method executes a SQL query to fetch the sequence information for a customer
    /// from the <c>CustomerSequence</c> table in the database.
    /// </remarks>
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
