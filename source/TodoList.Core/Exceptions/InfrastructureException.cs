namespace TodoList.Core.Exceptions
{
    using System;
    
    public class InfrastructureException : Exception
    {
        public InfrastructureException(string message) : base(message)
        {
        }
    }
}