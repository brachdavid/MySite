namespace MySite.Components.Models
{
    /// <summary>
    /// Třída reprezentující druh webové aplikace
    /// </summary>
    public class ProjectType
    {
        /// <summary>
        /// Název webové aplikace
        /// </summary>
        public required string Title { get; set; }
        /// <summary>
        /// Krátký popis webové aplikace
        /// </summary>
        public required string ShortDescription { get; set; }
        /// <summary>
        /// Detailní popis webové aplikace
        /// </summary>
        public required string DetailedDescription { get; set; }
        public required string IconClass { get; set; }
    }
}
