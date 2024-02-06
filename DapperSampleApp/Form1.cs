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
    private void IncrementCurrentSequenceButton_Click(object sender, EventArgs e)
    {
        InvoiceOperations operations = new InvoiceOperations();
        Customers customer = customersBindingList[customersBindingSource.Position];
        operations.SetSequence(customer);
    }


}
