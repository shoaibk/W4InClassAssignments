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
        try
        {
            var client = new HttpClient();
            var items = await client.GetFromJsonAsync<List<Product>>("https://fakestoreapi.com/products");

            if (items != null)
            {
                Products.Clear();
                foreach (var item in items)
                {
                    Products.Add(item);
                }
            }    
        } catch (Exception ex)
        {
            Console.WriteLine($"Failed to laod products: {ex.Message}");
        }
        
    }
    
}