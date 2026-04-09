namespace BarsboldApp.Models
{
    public class ItemModel
    {
        public string Titre { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public DateTime DateAjout { get; set; } = DateTime.Now;
    }
}
