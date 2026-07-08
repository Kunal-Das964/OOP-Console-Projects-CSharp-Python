using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services
{
    public static class Validation
    {
        public static bool ValidatePhone(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return false;
            }

            foreach (char c in phone)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            return phone.Length == 10;
        }

       
        private const string EmailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            return Regex.IsMatch(email, EmailPattern);
        }
    }
}
