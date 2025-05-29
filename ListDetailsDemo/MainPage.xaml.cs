using ListDetailsDemo.Models;
using ListDetailsDemo.Views;

namespace ListDetailsDemo;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
        ItemList.ItemsSource = new List<Item>
        {
            // create new items
            new Item { Title = "Apples", Description = "2 apples" },
            new Item { Title = "Oranges", Description = "3 oranges" },
            new Item { Title = "Bananas", Description = "4 bananas" },
        };
    }


    private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        Shell.Current.GoToAsync(nameof(DetailPage), new Dictionary<string, object>
        {
            { "SelectedItem", new Item { Title = "Apples", Description = "2 apples" }},
        });
    }
}