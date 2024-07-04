namespace Assignment.Exceptions
{
    using System;

    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string type, object id) 
            : base($"Entity {type} was not found by id {id}") {}
    }
}
