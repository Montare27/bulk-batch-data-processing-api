namespace Assignment.Exceptions
{
    using System;

    public class AccountDoesNotExistException : Exception
    {
        public AccountDoesNotExistException(Guid id, string message = "") : base(message)
        {
            AccountId = id;
        }

        public Guid AccountId { get; set; }
    }
}
