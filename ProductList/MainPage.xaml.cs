using ProductList.ViewModels;
using ProductList.Views;

namespace ProductList;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
    }


    private async void OnProductSelected(object? sender, SelectionChangedEventArgs e)
    {
        var selectedProduct = e.CurrentSelection.FirstOrDefault();
        if (selectedProduct == null) return;
        
        await Shell.Current.GoToAsync(nameof(DetailPage), new Dictionary<string, object>
        {
            ["SelectedProduct"] = selectedProduct
        });

    }
}