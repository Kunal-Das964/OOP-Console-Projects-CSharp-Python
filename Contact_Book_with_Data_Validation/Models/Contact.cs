using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Contact
    {
        public string Name { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public string Category { get; private set; }

        public Contact(string name, string phone, string email, string category)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Category = category;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Name: {Name} | Phone: {Phone} | Email: {Email} | Category: {Category}");
        }
    }
}
