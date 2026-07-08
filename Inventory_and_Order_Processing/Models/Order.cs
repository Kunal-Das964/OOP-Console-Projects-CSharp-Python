using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Order
    {
        public string CustomerName { get; private set; }
        public DateTime Timestamp { get; private set; }
        private List<Product> _products = new List<Product>();

        public Order(string customerName)
        {
            CustomerName = customerName;
            Timestamp = DateTime.Now;
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public double CalculateTotal()
        {
            double total = 0;
            foreach (Product product in _products)
            {
                total += product.Price * product.Quantity;
            }
            return total;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Order for: {CustomerName} | Time: {Timestamp}");
            foreach (Product product in _products)
            {
                product.DisplayInfo();
            }
            Console.WriteLine($"Order Total: {CalculateTotal()}");
        }
        public List<Product> GetProducts()
        {
            return _products;
        }
    }
}
