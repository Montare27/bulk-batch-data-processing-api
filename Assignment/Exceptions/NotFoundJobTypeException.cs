namespace Assignment.Exceptions
{
    using System;

    public class NotFoundJobTypeException : Exception
    {
        public NotFoundJobTypeException(string message) : base(message) {}
    }
}
