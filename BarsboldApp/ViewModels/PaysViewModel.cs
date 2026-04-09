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
    private string _errorMessage;
    private List<Country> _tousLesPays = new();
    private string _filtreActif = "All";
    
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

    public string ErrorMessage
    {
        get => _errorMessage;
        set
        {
            _errorMessage = value;
            OnPropertyChanged();
        }
    }

    public string FiltreActif
    {
        get => _filtreActif;
        set
        {
            _filtreActif = value;
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
        ErrorMessage = string.Empty;

        try
        {
            var paysTelecharges = await _apiService.GetCountriesAsync();
            
            _tousLesPays = paysTelecharges;
            
            FiltrerParContinent(_filtreActif);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Erreur : {ex.Message}";
            System.Diagnostics.Debug.WriteLine(ErrorMessage);
        }
        finally
        {
            IsLoading = false;
        }
    }

    public void FiltrerParContinent(string continent)
    {
        FiltreActif = continent;
        
        var paysFiltres = continent == "All" 
            ? _tousLesPays 
            : _tousLesPays.Where(p => p.Region.Equals(continent, StringComparison.OrdinalIgnoreCase)).ToList();

        ListePays.Clear();
        foreach (var pays in paysFiltres)
        {
            ListePays.Add(pays);
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}