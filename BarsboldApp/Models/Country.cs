namespace BarsboldApp.Models;

public class Country
{
    public int Id { get; set; }
    public string Name { get; set; } 
    public string Capital { get; set; } 
    public long Population { get; set; } 
    public string Region { get; set; } 
    

    public CountryMedia Media { get; set; } 
}


public class CountryMedia
{
    public string Flag { get; set; } 
}