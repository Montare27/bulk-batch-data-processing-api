namespace Assignment.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class JobType
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [Required(ErrorMessage = "Type is required")]
        [MinLength(1, ErrorMessage = "Name must be longer that 1 chars")] 
        [MaxLength(50, ErrorMessage = "Name must be shorter than 50 chars")]
        public string Name { get; set; } = string.Empty;
    }
}
