namespace MySite.Components.Models
{
    public class CooperationStep
    {
        /// <summary>
        /// Číslo kroku
        /// </summary>
        public required int Order { get; set; }
        /// <summary>
        /// Název kroku
        /// </summary>
        public required string StepName { get; set; }
        /// <summary>
        /// Popis kroku
        /// </summary>
        public required string StepDescription { get; set; }
    }
}
