namespace Assignment.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class JobItem
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [Required]
        public Guid AccountId { get; set; }
        
        [Required]
        public Guid DestinationAccountId { get; set; }
        
        [Required]
        public decimal Amount { get; set; }
    }
}
