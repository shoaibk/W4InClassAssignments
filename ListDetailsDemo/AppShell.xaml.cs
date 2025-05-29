using ListDetailsDemo.Views;

namespace ListDetailsDemo;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));
    }
}