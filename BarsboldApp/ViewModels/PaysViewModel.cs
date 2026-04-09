using System.Collections.ObjectModel;
using BarsboldApp.Models;
using BarsboldApp.Services;

namespace BarsboldApp.ViewModels;

public class PaysViewModel
{
    public ObservableCollection<Country> ListePays { get; set; } = new();
}