namespace Assignment.Validation
{
    using Assignment.Models;

    public class JobTypeValidator : IValidator<JobType>
    {

        public bool Validate(JobType item)
        {
            return !string.IsNullOrWhiteSpace(item.Name) &&
                   !string.IsNullOrEmpty(item.Name) &&
                   item.Name.Length >= 1 &&
                   item.Name.Length <= 50;
        }
    }
}
