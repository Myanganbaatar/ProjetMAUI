using BarsboldApp.ViewModels;

namespace BarsboldApp.Views;

public partial class AjoutPage : ContentPage
{
    public AjoutPage()
    {
        InitializeComponent();
        BindingContext = new AjoutViewModel();
    }

    private void OnAjouterPressed(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            button.BackgroundColor = Color.FromArgb("#0051D5"); // Couleur plus foncée (Premium)
            button.Scale = 0.97; // Légère réduction de taille
        }
    }

    private void OnAjouterReleased(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            button.BackgroundColor = Color.FromArgb("#007AFF"); // Couleur originale iOS
            button.Scale = 1.0; // Taille normale
        }
    }
}
