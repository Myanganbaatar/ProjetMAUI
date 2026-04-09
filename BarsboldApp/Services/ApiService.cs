using System.Net.Http.Json;
using BarsboldApp.Models;

namespace BarsboldApp.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient();
    }
}