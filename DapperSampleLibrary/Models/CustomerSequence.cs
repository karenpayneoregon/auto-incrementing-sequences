using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DapperSampleLibrary.Models;
public class CustomerSequence : INotifyPropertyChanged
{
    private int _customerIdentifier;
    private string? _currentSequenceValue;
    private string? _sequencePreFix;

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

    public string? SequencePreFix
    {
        get => _sequencePreFix;
        set
        {
            if (value == _sequencePreFix) return;
            _sequencePreFix = value;
            OnPropertyChanged();
        }
    }

    public override string ToString() => $"{SequencePreFix}{CurrentSequenceValue}";
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

