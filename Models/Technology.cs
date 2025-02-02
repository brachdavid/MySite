namespace MySite.Models
{
    public class Technology
    {
        /// <summary>
        /// Název technologie
        /// </summary>
        public required string Name { get; set; }
        /// <summary>
        /// URL adresa vedoucí k obrázku dané technologie
        /// </summary>
        public required string ImageUrl { get; set; }
    }
}
