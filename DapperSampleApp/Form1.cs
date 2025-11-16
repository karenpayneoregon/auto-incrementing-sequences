using System.ComponentModel;
using DapperSampleApp.Classes;
using DapperSampleApp.Models;

namespace DapperSampleApp;

public partial class Form1 : Form
{
    private BindingList<Customers> customersBindingList;
    private BindingSource customersBindingSource = new();
    public Form1()
    {
        InitializeComponent();
        Shown += Form1_Shown;
    }

    private void Form1_Shown(object? sender, EventArgs e)
    {
        InvoiceOperations operations = new InvoiceOperations();

        customersBindingList = new BindingList<Customers>(operations.CustomersList());
        customersBindingSource.DataSource = customersBindingList;
        CompanyListBox.DataSource = customersBindingSource;

        // since some CurrentSequenceValue can be null show (not set)
        CurrentSequenceLabel.DataBindings.Add("Text",
            customersBindingSource,
            nameof(Customers.Current),
            true,
            DataSourceUpdateMode.OnPropertyChanged, "");

    }

    /// <summary>
    /// This method retrieves the currently selected customer from the binding list and updates
    /// their sequence value using the <see cref="InvoiceOperations.SetSequence"/> method.
    /// </summary>
    private void IncrementCurrentSequenceButton_Click(object sender, EventArgs e)
    {
        InvoiceOperations operations = new InvoiceOperations();
        Customers customer = customersBindingList[customersBindingSource.Position];
        operations.SetSequence(customer);
    }
    /// <summary>
    /// Resets the current sequence value for the selected customer and updates the UI accordingly.
    /// </summary>
    private void ResetCurrentButton_Click(object sender, EventArgs e)
    {
        InvoiceOperations operations = new InvoiceOperations();
        Customers customer = customersBindingList[customersBindingSource.Position];
        operations.ResetSequence(customer.Id);

        var result = operations.GetSCustomers(customer.Id);
        if (result is not null)
        {
            customer.CurrentSequenceValue = result.CurrentSequenceValue;
        }
        
    }
}
