namespace TodoList.Core.Exceptions
{
    using System;

    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message)
        { }
    }
}