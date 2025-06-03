using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductList.Models;
using ProductList.ViewModels;

namespace ProductList.Views;

[QueryProperty(nameof(SelectedProduct), nameof(SelectedProduct)), ]
public partial class DetailPage : ContentPage
{
    private DetailPageViewModel _viewModel = new();
    public Product SelectedProduct
    {
        set => BindingContext = value;
    }
    public DetailPage()
    {
        InitializeComponent();
        BindingContext = _viewModel;
    }
}