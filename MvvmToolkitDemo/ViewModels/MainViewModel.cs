using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MvvmToolkitDemo.ViewModels;

public partial class MainViewModel: ObservableObject
{
    [ObservableProperty]
    private int _count;

    [RelayCommand]
    private void Increment()
    {
        Count++;
    }
    
}