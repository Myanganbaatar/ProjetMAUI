using BarsboldApp.Models;
using BarsboldApp.ViewModels;

namespace BarsboldApp.Views;

public partial class ProfilPage : ContentPage
{
    private readonly PaysViewModel _viewModel;

    public ProfilPage(PaysViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        
        // On assigne les BindingContexts aux sections correspondantes
        ItemsSection.BindingContext = new ProfilViewModel();
        PaysSection.BindingContext = _viewModel;
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        
        // On recharge les pays si la liste est vide pour toujours avoir du contenu
        if (_viewModel.ListePays.Count == 0)
        {
            await _viewModel.ChargerPaysAsync();
        }
    }

    
    private async void SurPaysSelectionne(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Country paysClique)
        {
            // On désélectionne l'élément visuellement
            ((CollectionView)sender).SelectedItem = null;

            // On navigue vers la page de détail
            await Navigation.PushAsync(new PaysDetailPage(paysClique));
        }
    }
}