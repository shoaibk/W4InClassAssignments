using ProductList.ViewModels;

namespace ProductList;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
        BindingContext =  new MainPageViewModel();
    }

   
}