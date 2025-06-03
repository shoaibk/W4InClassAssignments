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

    public string Title => Product.Title;
    public string Description => Product.Description;
    public string Image => Product.Image;

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