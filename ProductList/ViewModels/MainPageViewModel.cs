using System.Collections.ObjectModel;
using System.Net.Http.Json;
using ProductList.Models;

namespace ProductList.ViewModels;

public class MainPageViewModel
{
    public ObservableCollection<Product> Products { get; set; } = new();

    public MainPageViewModel()
    {
        LoadProducts();
    }
    
    private async void LoadProducts()
    {
        var client = new HttpClient();
        Products = await client.GetFromJsonAsync<ObservableCollection<Product>>("https://fakestoreapi.com/products");
    }
    
}