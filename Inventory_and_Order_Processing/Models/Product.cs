using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Product
    {
        public string Name { get; private set; }
        public double Price { get; private set; }
        public int Quantity { get; private set; }

        public Product(string name, double price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public double GetPrice()
        {
            return Price;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Product: {Name} | Price: {Price} | Quantity: {Quantity}");
        }
        public void ReduceQuantity(int amount)
        {
            Quantity -= amount;
        }

        public void IncreaseQuantity(int amount)
        {
            Quantity += amount;
        }
    }
}
