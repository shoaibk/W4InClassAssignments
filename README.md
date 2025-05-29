# Character Counter App

We will implement a character counter app. 
- There is a text edit field, where you can type in some text. 
- Then there is a counter that shows the length of the string that you typed in.
- Those two fields are internally connected. We call it binding. 
- The view of one element updates automatically in response to some changes of the other.

It will look like this:

<img width="300" alt="Screenshot 2025-05-26 at 4 50 43 AM" src="https://gist.github.com/user-attachments/assets/9d38b895-07f9-486e-90c9-927767575d2a" />

## Create new solution
Create a new Solution. Name it "W4CharaterCounter".

## MainPage.xaml
```xaml
<Grid RowSpacing="20" Margin="20" RowDefinitions="30,100,50,*">
        
        <Label Text="Enter your text:" 
               FontSize="20" />

        <Entry Placeholder="Type here" BackgroundColor="Aquamarine"
               Grid.Row="1"/>

        <Label Text="Text length" Grid.Row="2" />
        <Label 
               BackgroundColor="Beige"
               HorizontalTextAlignment="Center"
               VerticalTextAlignment="Center"
               FontSize="24" 
               Grid.Row="3"/>
</Grid>
```

## Implement with event listener
Associate variable names to the Entry and Label.

```xml
<Grid RowSpacing="20" Margin="20" RowDefinitions="30,100,50,*">
        
        <Label Text="Enter your text:" 
               FontSize="20" />
        
        <Entry 
            x:Name="TheTextEntry"
            Placeholder="Type here" 
            BackgroundColor="Aquamarine"
            TextChanged="TheTextEntry_OnTextChanged"
            Grid.Row="1"/>

        <Label Text="Text length" Grid.Row="2" />
        <Label 
            x:Name="TheLabel"
            BackgroundColor="Beige"
            HorizontalTextAlignment="Center"
            VerticalTextAlignment="Center"
            FontSize="24" 
            Grid.Row="3"/>
    </Grid>
```

Now in MainPage.xaml.cs, add this listener:
```cs
    private void TheTextEntry_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        TheLabel.Text = TheTextEntry.Text.Length.ToString();
    }
```

## TextViewModel.cs: ViewModel

- create a folder called ViewModels
- create a file TextViewModel.cs
- extend the TextViewModel class from INotifyPropertyChanged
- implement the methods of the Interface INotifyPropertyChanged

The class will look like this:

```cs
namespace W4CharaterCounter.ViewModels;

public class TextViewModel: INotifyPropertyChanged
{   
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
```

OnPropertyChanged method facilitates the change of state from one element to another.

### Add Text Property in TextViewModel.cs

We need a property that will carry information about what we typed into the Edit field. That same information will be used to update the label of the length of the string. Let's call it Text property.

Let's add Text property at the beginning of the TextViewModel file:

```cs
public string? Text { get; set; }
```

### Add Data Binding in MainPage.xaml

Now add data binding to the elements in MainPage.xaml:

```xaml
<Grid RowSpacing="20" Margin="20" RowDefinitions="30,100,50,*">
        
        <Label Text="Enter your text:" 
               FontSize="20" />
        
        <Entry Placeholder="Type here" BackgroundColor="Aquamarine"
               Text="{Binding Text}" Grid.Row="1"/>

        <Label Text="Text length" Grid.Row="2" />
        <Label Text="{Binding Text.Length}" 
               BackgroundColor="Beige"
               HorizontalTextAlignment="Center"
               VerticalTextAlignment="Center"
               FontSize="24" 
               Grid.Row="3"/>
    </Grid>
```

Now run the app. Notice that the counter label does not update. Why? Because we did not actually propagate the changes.

### Update MainPage.xaml.cs

We need to use the BindingContext of the MainPage to be the TextViewModel.cs class.

```cs
using W4CharaterCounter.ViewModels;

namespace W4CharaterCounter;

public partial class MainPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new TextViewModel();

    }
}
```

### Update TextViewModel.cs with Text Property
Update TextViewModel.cs as follows:

```cs
    private string? _text;
    public string? Text
    {
        get => _text;
        set => SetField(ref _text, value);
    }
```

If you run the app again, it should correctly show the count of the Text property.

# List & Detail Page App

- Creat a new Solution called ListDetailsDemo
- This app will show a list of items on the page.
- The list will be bound to a List data source or model
- Each element of the list can be a complex object. On the listview, we only show a small part of the object's information
- When you click on any item on the list, it should open another page. That page will show the detail information about the item.

<img width="300" alt="Screenshot 2025-05-26 at 8 01 02 AM" src="https://gist.github.com/user-attachments/assets/00e8e463-e487-435f-8ff1-2883651733e8" />

## Add a Model: Item.cs file

- Create a folder Models
- Add a class Item.cs
- This is the data model of our app. Each item in our listview will have the shape of this object. The view of the list will be extracted somehow from this object's data.

```cs
namespace ListDetailsDemo.Models;

public class Item
{
    public string Title { get; set; }
    public string Description { get; set; }
}
```

## MainPage.xaml

Change the Title of the page to "Item List".

Delete the existing content of the page and replace with the following. Here, ItemList is the list we will create in CSharp file.

```cs
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="ListDetailsDemo.Views.MainPage"
             Title="Item List">
    
    
    <CollectionView x:Name="ItemList">
        <CollectionView.ItemTemplate>
            <DataTemplate>
                <Label Text="{Binding Title}" 
                       FontSize="20" 
                       BackgroundColor="BlanchedAlmond" 
                       VerticalTextAlignment="Center"
                       Padding="10"
                       Margin="10"/>
            </DataTemplate>
        </CollectionView.ItemTemplate>
    </CollectionView>

</ContentPage>
```

## MainPage.xaml.cs

Now in the MainPage.xaml.cs file, populate ItemList with some initial values.

```cs
using ListDetailsDemo.Models;

namespace ListDetailsDemo.Views;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
        ItemList.ItemsSource = new List<Item>
        {
            new Item { Title = "Item 1", Description = "Description 1" },
            new Item { Title = "Item 2", Description = "Description 2" },
            new Item { Title = "Item 3", Description = "Description 3" },
        };
    }

}
```

## Adding Navigation
Now we would like to navigate to a details page when we click on a particular item on this list.

### DetailPage
- Create a folder called Views
- Add a ContentPage called DetailPage in the folder Views

Here is the content of the DetailPage.xaml
```
<?xml version="1.0" encoding="utf-8"?>

<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="ListDetailsDemo.Views.DetailPage" Title="Detail">
    <StackLayout Padding="20">
        <Label x:Name="TitleLabel" FontSize="24" FontAttributes="Bold" />
        <Label x:Name="DescriptionLabel" FontSize="18" Margin="0,10,0,0" />
    </StackLayout>
</ContentPage>
```

Note that TitleLabel and DescriptionLabel are variables that can now be accessed from the C# file.

### Add Routing for Detail Page
In AppShell.xaml.cs file, Add Routing like this:

```cs
using ListDetailsDemo.Views;

namespace ListDetailsDemo;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(DetailPage), typeof(Views.DetailPage));
    }
}
```

### Update DetailPage.xaml.cs

```cs
using ListDetailsDemo.Models;

namespace ListDetailsDemo.Views;

[QueryProperty(nameof(SelectedItem), "SelectedItem")]
public partial class DetailPage : ContentPage
{
    private Item _selectedItem;
    public Item SelectedItem
    {
        get => _selectedItem;
        set
        {
            _selectedItem = value;
            TitleLabel.Text = value.Title;
            DescriptionLabel.Text = value.Description;
        }
    }
    public DetailPage()
    {
        InitializeComponent();
    }
}
```

### Add event listener on the list

The CollectionView tag should have the properties for listening to selection:

```xml
<CollectionView x:Name="ItemList" SelectionChanged="OnSelectionChanged" SelectionMode="Single">
```

Now, in MainPage.xaml.cs file, add the OnSelectionChanged method, if not added by the IDE:

```cs
using ListDetailsDemo.Models;

namespace ListDetailsDemo.Views;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
        ItemList.ItemsSource = new List<Item>
        {
            new Item { Title = "Item 1", Description = "Description 1" },
            new Item { Title = "Item 2", Description = "Description 2" },
            new Item { Title = "Item 3", Description = "Description 3" },
        };
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(DetailPage), new Dictionary<string, object>
        {
            { nameof(DetailPage.SelectedItem), new Item{Title = "Item x", Description = "Description xxx"} }
        });
        
        
    }
}
```

### Connect selectedItem to the DetailPage

Right now, a hard-coded data is displayed on the Detail Page. If we want to display the actual list item's data, we have to send the selected item to the DetailPage when we call GoToAsync

```cs
using ListDetailsDemo.Models;

namespace ListDetailsDemo.Views;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
        ItemList.ItemsSource = new List<Item>
        {
            new Item { Title = "Item 1", Description = "Description 1" },
            new Item { Title = "Item 2", Description = "Description 2" },
            new Item { Title = "Item 3", Description = "Description 3" },
        };
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = e.CurrentSelection.FirstOrDefault() as Item;
        if (selectedItem != null)
        {
            await Shell.Current.GoToAsync("DetailPage", new Dictionary<string, object>
            {
                { "SelectedItem", selectedItem }
            });
        
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}
```

# ProductList App with http get
We will show this product list using http:

https://fakestoreapi.com/products

Here’s a basic example of a .NET MAUI app that fetches a list of items from a public API (like the Fake Store API) and displays a list with images, titles, and descriptions.

### Features

Gets data from an HTTP API

Displays list with images, titles, and descriptions

Uses CollectionView in .NET MAUI

MVVM architecture (simple)

### Project Structure

MainPage.xaml – UI for the list

MainPageViewModel.cs – Fetches and stores data

Product.cs – Model for the API data


### Create Model: Product.cs
Create a Models folder and add this file:

```csharp
namespace MauiProductList.Models;

public class Product
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
}
```


### ViewModel: MainPageViewModel.cs
Create a ViewModels folder and add:


```cs
using System.Collections.ObjectModel;
using System.Net.Http.Json;
using MauiProductList.Models;

namespace MauiProductList.ViewModels;

public class MainPageViewModel
{
    public ObservableCollection<Product> Products { get; set; } = new();

    public MainPageViewModel()
    {
        LoadData();
    }

    private async void LoadData()
    {
        try
        {
            using var client = new HttpClient();
            var products = await client.GetFromJsonAsync<List<Product>>("https://fakestoreapi.com/products");

            if (products != null)
            {
                foreach (var product in products)
                    Products.Add(product);
            }
        }
        catch (Exception ex)
        {
            // Handle errors here
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
```

### MainPage.xaml
Update MainPage.xaml:

```xml
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:viewModels="clr-namespace:MauiProductList.ViewModels"
             x:Class="MauiProductList.MainPage">

    <ContentPage.BindingContext>
        <viewModels:MainPageViewModel />
    </ContentPage.BindingContext>

    <CollectionView ItemsSource="{Binding Products}" Margin="10">
        <CollectionView.ItemTemplate>
            <DataTemplate>
                <Frame BorderColor="LightGray" Padding="10" Margin="5" CornerRadius="10">
                    <Grid ColumnDefinitions="100,*,Auto" RowDefinitions="Auto,Auto">
                        <Image Source="{Binding Image}" WidthRequest="80" HeightRequest="80" Grid.RowSpan="2"/>
                        <Label Text="{Binding Title}" FontAttributes="Bold" Grid.Column="1" LineBreakMode="TailTruncation"/>
                        <Label Text="{Binding Description}" Grid.Column="1" Grid.Row="1"
                               FontSize="12" LineBreakMode="TailTruncation" MaxLines="3"/>
                    </Grid>
                </Frame>
            </DataTemplate>
        </CollectionView.ItemTemplate>
    </CollectionView>
</ContentPage>
```
