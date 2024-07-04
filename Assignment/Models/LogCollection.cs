namespace Assignment.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class LogCollection
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        public Guid JobId { get; set; }

        public ICollection<LogEntry> Logs { get; set; } = new List<LogEntry>();
    }
}
