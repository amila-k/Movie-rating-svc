using System;

namespace Infrastructure.CustomExceptions
{
    public class ShowException : Exception
    {
        public ShowException(string message) : base(message) { }
    }
}
