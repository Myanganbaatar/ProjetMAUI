using System.Net.Http.Json;
using BarsboldApp.Models;

namespace BarsboldApp.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient();
        _httpClient.Timeout = TimeSpan.FromSeconds(30);
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");
    }

    
    public async Task<List<Country>> GetCountriesAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("https://api.sampleapis.com/countries/countries");
            
            if (response.IsSuccessStatusCode)
            {
                var list = await response.Content.ReadFromJsonAsync<List<Country>>();
                if (list != null && list.Count > 0) return list;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"API Error/Timeout: {ex.Message}");
        }
        
        
        return ObtenirPaysTest();
    }

    private List<Country> ObtenirPaysTest()
    {
        return new List<Country>
        {
            new Country { Name = "France (Test)", Capital = "Paris", Population = 67000000, Media = new CountryMedia { Flag = "https://flagcdn.com/w320/fr.png" } },
            new Country { Name = "Mongolie (Test)", Capital = "Oulan-Bator", Population = 3300000, Media = new CountryMedia { Flag = "https://flagcdn.com/w320/mn.png" } },
            new Country { Name = "Japon (Test)", Capital = "Tokyo", Population = 125000000, Media = new CountryMedia { Flag = "https://flagcdn.com/w320/jp.png" } }
        };
    }
}