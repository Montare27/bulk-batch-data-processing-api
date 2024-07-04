namespace Assignment.Validation
{
    using Assignment.Models;

    public class JobValidator : IValidator<Job>
    {
        public bool Validate(Job item)
        {
            return item.Type != null && 
                   item.Items != null;
        }
    }
}
