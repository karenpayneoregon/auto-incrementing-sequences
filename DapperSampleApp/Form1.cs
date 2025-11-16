using System.ComponentModel;
using DapperSampleApp.Classes;
using DapperSampleLibrary.Models;
using DapperSampleLibrary.Classes;

namespace DapperSampleApp;

public partial class Form1 : Form
{
    private BindingList<Customers> _customersBindingList;
    private BindingSource _customersBindingSource = new();
    
    public Form1()
    {
        InitializeComponent();
        Shown += Form1_Shown;
    }

    /// <summary>
    /// This method initializes the customer list by fetching data asynchronously
    /// and binds it to the UI components, such as <see cref="CompanyListBox"/> and <see cref="CurrentSequenceLabel"/>.
    /// </summary>
    /// <remarks>
    /// If an exception occurs during the data retrieval or binding process, 
    /// it is caught and displayed using the <see cref="Dialogs.ErrorBox"/> method.
    /// </remarks>
    private async void Form1_Shown(object? sender, EventArgs e)
    {
        try
        {
            
            InvoiceOperations operations = new();

            var customers = await operations.CustomersList();
            
            _customersBindingList = new BindingList<Customers>(customers);
            _customersBindingSource.DataSource = _customersBindingList;
            CompanyListBox.DataSource = _customersBindingSource;

            // since some CurrentSequenceValue can be null show (not set)
            CurrentSequenceLabel.DataBindings.Add("Text",
                _customersBindingSource,
                nameof(Customers.Current),
                true,
                DataSourceUpdateMode.OnPropertyChanged, "");
            
        }
        catch (Exception ex)
        {
            Dialogs.ErrorBox(this, ex);
        }
    }

    /// <summary>
    /// This method retrieves the currently selected customer from the binding list and updates
    /// their sequence value using the <see cref="InvoiceOperations.SetSequence"/> method.
    /// </summary>
    private void IncrementCurrentSequenceButton_Click(object sender, EventArgs e)
    {
        InvoiceOperations operations = new();
        Customers customer = _customersBindingList[_customersBindingSource.Position];
        operations.SetSequence(customer);
    }
    
    /// <summary>
    /// Resets the current sequence value for the selected customer and updates the UI accordingly.
    /// </summary>
    private void ResetCurrentButton_Click(object sender, EventArgs e)
    {
        InvoiceOperations operations = new();
        Customers customer = _customersBindingList[_customersBindingSource.Position];
        operations.ResetSequence(customer.Id);

        var result = operations.GetCustomers(customer.Id);
        if (result is not null)
        {
            customer.CurrentSequenceValue = result.CurrentSequenceValue;
        }
        
    }
    
}
