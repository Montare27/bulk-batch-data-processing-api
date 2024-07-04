namespace Assignment.Validation
{

    public interface IValidator<in T> where T : class
    {
        bool Validate(T item);
    }
}
