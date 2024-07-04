namespace Assignment.Exceptions
{
    using System;

    public class NotEnoughMoneyException : Exception
    {
        public NotEnoughMoneyException(Guid id, string message = "") : base(message) 
        {
            AccountId = id;
        }
        
        public Guid AccountId { get; set; }
    }
}
