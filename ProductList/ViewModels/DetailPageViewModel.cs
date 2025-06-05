using System.ComponentModel;
using System.Runtime.CompilerServices;
using ProductList.Models;

namespace ProductList.ViewModels;

public class DetailPageViewModel: INotifyPropertyChanged
{
    private Product _product;

    public Product Product
    {
        get => _product;
        set
        {
            _product = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Description));
            OnPropertyChanged(nameof(Image));
        }
    }
    
    public string Title => Product?.Title ?? string.Empty;
    public string Description => Product?.Description ?? string.Empty;
    public string Image => Product?.Image ?? string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
}