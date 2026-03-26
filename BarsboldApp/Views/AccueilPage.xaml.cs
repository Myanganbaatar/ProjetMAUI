using BarsboldApp.ViewModels;

namespace BarsboldApp.Views;

public partial class AccueilPage : ContentPage
{
    public AccueilPage()
    {
        InitializeComponent();
        BindingContext = new MainViewModel();
    }

    private async void OnGifButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GifPage());
    }
}
