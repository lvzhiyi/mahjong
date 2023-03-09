using System;

namespace cambrian.common
{
    public class DataAccessException : SystemException
    {
        public DataAccessException(string message) : base(message)
        {
        }
    }
}