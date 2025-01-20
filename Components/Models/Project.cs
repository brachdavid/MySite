namespace MySite.Components.Models
{
    public class Project
    {
        /// <summary>
        /// Název projektu
        /// </summary>
        public required string Name { get; set; }
        /// <summary>
        /// URL adresa daného projektu
        /// </summary>
        public required string Link { get; set; }
        /// <summary>
        /// Popis projektu
        /// </summary>
        public required string Description { get; set; }
        /// <summary>
        /// Technologie použité při tvorbě projektu
        /// </summary>
        public required string Technologies { get; set; }
        /// <summary>
        /// URL adresa vedoucí k náhledovému obrázku
        /// </summary>
        public required string ImagePath { get; set; }
    }
}
