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
        
        ItemsSection.BindingContext = new ProfilViewModel();
        PaysSection.BindingContext = _viewModel;
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        
        
        if (_viewModel.ListePays.Count == 0)
        {
            await _viewModel.ChargerPaysAsync();
        }
    }

    
    private async void SurPaysSelectionne(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Country paysClique)
        {
            
            ((CollectionView)sender).SelectedItem = null;

           
            await Navigation.PushAsync(new PaysDetailPage(paysClique));
        }
    }
}