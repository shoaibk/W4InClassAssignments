using ChracterCounter.ViewModels;

namespace ChracterCounter;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
        BindingContext = new TextViewModel();
    }
}