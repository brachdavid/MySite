namespace MySite.Models
{
    public class ReferenceProject
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty; // Popisek se použije jen pro nekomerční projekty
        public string Technologies { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public bool IsCommercial { get; set; } // Určuje, zda jde o komerční projekt
    }
}
