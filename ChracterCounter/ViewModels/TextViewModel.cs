using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ChracterCounter.ViewModels;

public class TextViewModel:INotifyPropertyChanged
{
    private string _text;
    public string Text { 
        get => _text;
        set => SetField(ref _text, value);
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}