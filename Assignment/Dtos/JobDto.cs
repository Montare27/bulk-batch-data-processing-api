namespace Assignment.Dtos
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Dto for entity Job
    /// </summary>
    public class JobDto
    {
        [Required(ErrorMessage = "Type is required")]
        [MinLength(1, ErrorMessage = "Name must be longer that 1 chars")] 
        [MaxLength(50, ErrorMessage = "Name must be shorter than 50 chars")]
        public string JobType { get; set; } = string.Empty;

        public ICollection<JobItemDto> Items { get; set; } = new List<JobItemDto>();
    }
}
