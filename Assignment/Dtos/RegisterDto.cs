namespace Assignment.Dtos
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Dto for entity Account
    /// </summary>
    public class RegisterDto
    {
        [MinLength(3), MaxLength(20)]
        public string Username { get; set; } = string.Empty;
        
        [MinLength(3), MaxLength(20)]
        public string Password { get; set; } = string.Empty;
        
        public decimal Amount { get; set; }
    }
}
