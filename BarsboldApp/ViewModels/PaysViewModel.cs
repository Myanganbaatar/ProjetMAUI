using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BarsboldApp.Models;
using BarsboldApp.Services;

namespace BarsboldApp.ViewModels;

public class PaysViewModel : INotifyPropertyChanged
{
    private readonly ApiService _apiService;
    private bool _isLoading;
    
    public ObservableCollection<Country> ListePays { get; set; } = new();

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged();
        }
    }

    public PaysViewModel(ApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task ChargerPaysAsync()
    {
        if (IsLoading) return;
        
        IsLoading = true;
        try
        {
            var paysTelecharges = await _apiService.GetCountriesAsync();
            
            ListePays.Clear();
            foreach (var pays in paysTelecharges)
            {
                ListePays.Add(pays);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Erreur : {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}