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
    }
}