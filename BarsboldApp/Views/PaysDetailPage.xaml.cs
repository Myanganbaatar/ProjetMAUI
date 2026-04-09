using BarsboldApp.Models; 

namespace BarsboldApp.Views;

public partial class PaysDetailPage : ContentPage
{

    public PaysDetailPage(Country paysSelectionne)
    {
        InitializeComponent();
        
        BindingContext = paysSelectionne;
    }
}