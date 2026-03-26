using BarsboldApp.ViewModels;
using System.Timers;

namespace BarsboldApp.Views;

public partial class AccueilPage : ContentPage
{
    private System.Timers.Timer? _carouselTimer;

    public AccueilPage()
    {
        InitializeComponent();
        BindingContext = new MainViewModel();
        StartCarouselAutoScroll();
    }

    private void StartCarouselAutoScroll()
    {
        _carouselTimer = new System.Timers.Timer(3000);
        _carouselTimer.Elapsed += (s, e) =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (CarouselCountries.Position < (BindingContext as MainViewModel)!.CountryImages.Count - 1)
                {
                    CarouselCountries.Position++;
                }
                else
                {
                    CarouselCountries.Position = 0;
                }
            });
        };
        _carouselTimer.Start();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _carouselTimer?.Stop();
    }

    private async void OnGifButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GifPage());
    }
}
