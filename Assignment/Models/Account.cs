namespace Assignment.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Account
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [MinLength(3), MaxLength(20)]
        public string Username { get; set; } = string.Empty;
        
        public decimal Amount { get; set; }

        [MinLength(3), MaxLength(20)]
        public string Password { get; set; } = string.Empty;
    }
}
