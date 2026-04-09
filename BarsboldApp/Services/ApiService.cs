using System.Net.Http.Json;
using System.Text.Json.Serialization;
using BarsboldApp.Models;

namespace BarsboldApp.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient();
        // Timeout augmenté à 30s pour les connexions lentes
        _httpClient.Timeout = TimeSpan.FromSeconds(30);
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");
    }

    
    public async Task<List<Country>> GetCountriesAsync()
    {
        try
        {
            // Nouvelle URL vers RestCountries (stable et rapide)
            var url = "https://restcountries.com/v3.1/all?fields=name,capital,population,flags";
            var response = await _httpClient.GetAsync(url);
            
            if (response.IsSuccessStatusCode)
            {
                var restCountries = await response.Content.ReadFromJsonAsync<List<RestCountryDto>>();
                
                if (restCountries != null && restCountries.Count > 0)
                {
                    // On transforme les données de l'API vers notre modèle interne
                    return restCountries.Select(rc => new Country
                    {
                        Name = rc.Name?.Common ?? "Inconnu",
                        Capital = rc.Capital != null && rc.Capital.Length > 0 ? rc.Capital[0] : "N/A",
                        Population = rc.Population,
                        Media = new CountryMedia { Flag = rc.Flags?.Png }
                    }).OrderBy(c => c.Name).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"API Error: {ex.Message}");
        }
        
        // En cas d'échec complet, on garde le fallback Mock
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


internal class RestCountryDto
{
    [JsonPropertyName("name")]
    public RestName Name { get; set; }

    [JsonPropertyName("capital")]
    public string[] Capital { get; set; }

    [JsonPropertyName("population")]
    public long Population { get; set; }

    [JsonPropertyName("flags")]
    public RestFlags Flags { get; set; }
}

internal class RestName
{
    [JsonPropertyName("common")]
    public string Common { get; set; }
}

internal class RestFlags
{
    [JsonPropertyName("png")]
    public string Png { get; set; }
}