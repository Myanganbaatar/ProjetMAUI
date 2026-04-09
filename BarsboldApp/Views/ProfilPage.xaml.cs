using BarsboldApp.ViewModels;

namespace BarsboldApp.Views;

public partial class ProfilPage : ContentPage
{
    public ProfilPage()
    {
        InitializeComponent();
        BindingContext = new ProfilViewModel();
    }
}
