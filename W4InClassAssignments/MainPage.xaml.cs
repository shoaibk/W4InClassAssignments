namespace W4InClassAssignments;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
    }

    private void TheTextEntry_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        TheLabel.Text = TheTextEntry.Text.Length.ToString();
    }
}