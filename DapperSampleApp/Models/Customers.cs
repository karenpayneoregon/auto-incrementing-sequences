#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DapperSampleApp.Models;
public class Customers : INotifyPropertyChanged
{
    private int _customerIdentifier;
    private string _companyName;
    private string _sequencePreFix;
    private string? _currentSequenceValue;
    public int Id { get; set; }
    public int CustomerIdentifier
    {
        get => _customerIdentifier;
        set
        {
            if (value == _customerIdentifier) return;
            _customerIdentifier = value;
            OnPropertyChanged();
        }
    }

    public string CompanyName
    {
        get => _companyName;
        set
        {
            if (value == _companyName) return;
            _companyName = value;
            OnPropertyChanged();
        }
    }

    public string SequencePreFix
    {
        get => _sequencePreFix;
        set
        {
            if (value == _sequencePreFix) return;
            _sequencePreFix = value;
            OnPropertyChanged();
        }
    }

    public string? CurrentSequenceValue
    {
        get => _currentSequenceValue;
        set
        {
            if (value == _currentSequenceValue) return;
            _currentSequenceValue = value;
            OnPropertyChanged();
        }
    }

    public string Current => _currentSequenceValue is null ? $"{SequencePreFix}000000" : $"{SequencePreFix}{CurrentSequenceValue}";

    public override string ToString() => CompanyName;
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

