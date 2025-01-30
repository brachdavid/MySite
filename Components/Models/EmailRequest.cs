using System.ComponentModel.DataAnnotations;

namespace MySite.Components.Models
{
    /// <summary>
    /// Typ dotazu
    /// </summary>
    public enum QueryType
    {
        [Display(Name = "Tvorba webovek")]
        WebCreate = 1,
        [Display(Name = "Úprava webovek")]
        WebUpdate = 2,
        [Display(Name = "Správa webovek")]
        WebAdministation = 3,
        [Display(Name = "Tvůrčí psaní")]
        CreativeWriting = 4,
        [Display(Name = "Nabídka práce")]
        JobOffer = 5
    }
    public class EmailRequest
    {
        /// <summary>
        /// Jméno toho, kdo pokládá dotaz
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// E-mail toho, kdo pokládá dotaz
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Telefonní číslo toho, kdo pokládá dotaz
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Typ dotazu
        /// </summary>
        public QueryType QueryType { get; set; } = QueryType.WebCreate;

        /// <summary>
        /// Zpráva pro příjemce dotazu
        /// </summary>
        public string? Message { get; set; }
    }
}
