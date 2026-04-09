using System.Net.Http.Json;
using BarsboldApp.Models;

namespace BarsboldApp.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient();
        _httpClient.Timeout = TimeSpan.FromSeconds(10);
    }

    
    public async Task<List<Country>> GetCountriesAsync()
    {
        try
        {

            var response = await _httpClient.GetAsync("https://api.sampleapis.com/countries/countries");
            
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Country>>() ?? new List<Country>();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Erreur lors de l'appel API : {ex.Message}");
        }
        
        
        return new List<Country>();
    }
}