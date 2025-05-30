using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListDetailsDemo.Models;

namespace ListDetailsDemo.Views;

[QueryProperty(nameof(SelectedItem), nameof(SelectedItem))]
public partial class DetailPage : ContentPage
{
    private Item? _selectedItem;

    public Item? SelectedItem
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