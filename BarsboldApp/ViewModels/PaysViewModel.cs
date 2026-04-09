using System.Collections.ObjectModel;
using BarsboldApp.Models;
using BarsboldApp.Services;

namespace BarsboldApp.ViewModels;

public class PaysViewModel
{
    private readonly ApiService _apiService;
    
    
    public ObservableCollection<Country> ListePays { get; set; } = new();

    // On injecte notre service ici
    public PaysViewModel(ApiService apiService)
    {
        _apiService = apiService;
    }

    
    public async Task ChargerPaysAsync()
    {
        
        var paysTelecharges = await _apiService.GetCountriesAsync();
        
        
        ListePays.Clear();
        foreach (var pays in paysTelecharges)
        {
            ListePays.Add(pays);
        }
    }
}