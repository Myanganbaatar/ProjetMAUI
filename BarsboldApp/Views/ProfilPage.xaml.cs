using BarsboldApp.Models;
using BarsboldApp.ViewModels;
using Microsoft.Maui.ApplicationModel;

namespace BarsboldApp.Views;

public partial class ProfilPage : ContentPage
{
    private readonly PaysViewModel _viewModel;
    private bool _isNavigating = false;

    public ProfilPage(PaysViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        
        ItemsSection.BindingContext = new ProfilViewModel();
        PaysSection.BindingContext = _viewModel;
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        // Réinitialisation de l'état de navigation pour permettre de recliquer
        _isNavigating = false;
        
        if (_viewModel.ListePays.Count == 0)
        {
            Task.Run(async () => await _viewModel.ChargerPaysAsync());
        }
    }

    private async void SurPaysSelectionneTap(object sender, TappedEventArgs e)
    {
        // On évite les doubles clics
        if (_isNavigating) return;

        if (e.Parameter is Country paysClique)
        {
            _isNavigating = true;

            // Navigation immédiate sur le thread UI
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                try 
                {
                    await Navigation.PushAsync(new PaysDetailPage(paysClique));
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Erreur Navigation : {ex.Message}");
                    _isNavigating = false;
                }
            });
        }
    }

    private void OnContinentClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is string continent)
        {
            _viewModel.FiltrerParContinent(continent);
        }
    }
}