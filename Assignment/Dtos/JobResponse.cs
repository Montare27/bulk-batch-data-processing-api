namespace Assignment.Dtos
{
    using System;

    public class JobResponse
    {
        public Guid Id { get; set; }
        
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
