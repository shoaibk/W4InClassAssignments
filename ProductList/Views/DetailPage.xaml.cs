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
    private readonly DetailPageViewModel? viewModel;

    public Product SelectedProduct
    { 
        set => viewModel.Product = value;
        
    }
    public DetailPage()
    {
        InitializeComponent();
        viewModel = BindingContext as DetailPageViewModel;
    }
}