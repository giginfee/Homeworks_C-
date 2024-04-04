using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayProject.Exceptions
{
    public class FutureDateOfBirthException : Exception
    {

        public FutureDateOfBirthException(DateTime date) : base($"Date {date.ToShortDateString()} is in the future. ")
        {
        }
    }

   public class DistantDateOfBirthException : Exception
    {
        public DistantDateOfBirthException(DateTime date) : base($"Person born {date.ToShortDateString()} is probably not alive anymore. ")
        {
        }
    }

    public class InvalidEmailException : Exception
    {
        public InvalidEmailException() 
        {}
        public InvalidEmailException(string email) : base($"{email} is not a valid email address. ")
        {
        }
    }
}
