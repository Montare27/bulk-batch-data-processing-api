namespace Assignment.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class LogEntry
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        public Guid ItemId { get; set; }
        
        public bool Success { get; set; } = true;
        
        public string Description { get; set; } = string.Empty;
    }
}
