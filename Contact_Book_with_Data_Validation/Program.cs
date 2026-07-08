using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_Book_with_Data_Validation
{
    public class Program
    {
        static void Main(string[] args)
        {
            string[] testPhones = { "9876543210", "98765", "98765432ab", null };
            foreach (string p in testPhones)
            {
                Console.WriteLine($"{p} -> {Validation.ValidatePhone(p)}");
            }

            string[] testEmails = { "alice@example.com", "alice@example", "alice.com", "a@b.c", null };
            foreach (string e in testEmails)
            {
                Console.WriteLine($"{e} -> {Validation.ValidateEmail(e)}");
            }

            Console.ReadLine();
        }
    }
}
