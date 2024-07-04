namespace Assignment.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Job
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        public JobType Type { get; set; }

        public ICollection<JobItem> Items { get; set; }
    }
}
