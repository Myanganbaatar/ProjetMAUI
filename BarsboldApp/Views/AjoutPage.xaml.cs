using BarsboldApp.ViewModels;

namespace BarsboldApp.Views;

public partial class AjoutPage : ContentPage
{
    public AjoutPage()
    {
        InitializeComponent();
        BindingContext = new AjoutViewModel();
    }
}
