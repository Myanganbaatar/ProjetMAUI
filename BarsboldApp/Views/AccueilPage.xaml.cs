using BarsboldApp.ViewModels;

namespace BarsboldApp.Views;

public partial class AccueilPage : ContentPage
{
    public AccueilPage()
    {
        InitializeComponent();
        BindingContext = new MainViewModel();
    }
}
