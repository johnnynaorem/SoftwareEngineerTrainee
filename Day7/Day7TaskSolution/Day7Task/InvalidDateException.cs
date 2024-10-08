﻿

namespace Day7Task
{
    internal class InvalidDateException : Exception
    {
        string message;
        public InvalidDateException()
        {
            message = "The input date was greater than the current date. Please try again.";
        }
        public override string Message => message;

    }
}