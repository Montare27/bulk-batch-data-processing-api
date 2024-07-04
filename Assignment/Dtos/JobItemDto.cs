namespace Assignment.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Dto for entity JobItem
    /// </summary>
    public class JobItemDto
    {
        [Required]
        public Guid AccountId { get; set; }
        
        [Required]
        public Guid DestinationAccountId { get; set; }
        
        [Required]
        public decimal Amount { get; set; }
    }
}
