using DataStructures;
using Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Inventory
    {
        private Dictionary<string, Product> _stock = new Dictionary<string, Product>();
        private CustomStack<Order> _undoStack = new CustomStack<Order>();
        private CustomQueue<Order> _shippingQueue = new CustomQueue<Order>();
        private string _ordersFilepath;

        public Inventory(string ordersFilepath)
        {
            _ordersFilepath = ordersFilepath;
        }
        public void LoadStock(string filepath)
        {
            if (!File.Exists(filepath))
            {
                Console.WriteLine($"Stock file not found: {filepath}");
                return;
            }

            foreach (string rawLine in File.ReadAllLines(filepath))
            {
                string line = rawLine.Trim();
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                string[] parts = line.Split(',');
                if (parts.Length != 3)
                {
                    Console.WriteLine($"Skipped malformed stock line: {line}");
                    continue;
                }

                string name = parts[0];
                if (!double.TryParse(parts[1], out double price) || !int.TryParse(parts[2], out int quantity))
                {
                    Console.WriteLine($"Skipped malformed stock line: {line}");
                    continue;
                }

                _stock[name] = new Product(name, price, quantity);
            }
        }

        public void LogOrder(Order order)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_ordersFilepath));
            using (StreamWriter writer = new StreamWriter(_ordersFilepath, true)) // append
            {
                writer.WriteLine($"Customer: {order.CustomerName} | Time: {order.Timestamp} | Total: {order.CalculateTotal()}");
                foreach (Product product in order.GetProducts())
                {
                    writer.WriteLine($"  {product.Name},{product.Price},{product.Quantity}");
                }
                writer.WriteLine(new string('=', 40));
            }
        }

        public void LogUndoNote(Order order)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_ordersFilepath));
            using (StreamWriter writer = new StreamWriter(_ordersFilepath, true)) // append
            {
                writer.WriteLine($"NOTE: Order for {order.CustomerName} (originally placed at {order.Timestamp}) was UNDONE.");
                writer.WriteLine(new string('=', 40));
            }
        }
        public Order PlaceOrder(string customerName, List<(string ProductName, int Quantity)> requestedItems)
        {
            foreach (var item in requestedItems)
            {
                if (!_stock.ContainsKey(item.ProductName))
                {
                    Console.WriteLine($"Order rejected: '{item.ProductName}' does not exist in stock.");
                    return null;
                }
                Product availableProduct = _stock[item.ProductName];
                if (item.Quantity > availableProduct.Quantity)
                {
                    Console.WriteLine($"Order rejected: not enough stock for '{item.ProductName}' " +
                                       $"(requested {item.Quantity}, available {availableProduct.Quantity}).");
                    return null;
                }
            }

            Order order = new Order(customerName);
            foreach (var item in requestedItems)
            {
                Product stockProduct = _stock[item.ProductName];
                stockProduct.ReduceQuantity(item.Quantity);

                Product orderedProduct = new Product(item.ProductName, stockProduct.Price, item.Quantity);
                order.AddProduct(orderedProduct);
            }

            _undoStack.Push(order);
            _shippingQueue.Enqueue(order);

            LogOrder(order);
            Console.WriteLine($"Order placed successfully for {customerName}.");
            return order;
        }

        public Order UndoLastOrder()
        {
            Order order = _undoStack.Pop();
            if (order == null)
            {
                return null;
            }

            foreach (Product product in order.GetProducts())
            {
                if (_stock.ContainsKey(product.Name))
                {
                    _stock[product.Name].IncreaseQuantity(product.Quantity);
                }
            }

            LogUndoNote(order);

            Console.WriteLine($"Order for {order.CustomerName} undone. Stock restored.");
            return order;
        }

        public Order ShipNextOrder()
        {
            Order order = _shippingQueue.Dequeue();
            if (order == null)
            {
                return null;
            }

            Console.WriteLine($"Shipping order for {order.CustomerName}:");
            order.DisplayInfo();
            return order;
        }

        public List<Product> GetLowStockProducts()
        {
            List<Product> lowStock = new List<Product>();
            foreach (Product product in _stock.Values)
            {
                if (product.Quantity < 5)
                {
                    lowStock.Add(product);
                }
            }
            return lowStock;
        }

        public List<Product> GetAllProducts()
        {
            return new List<Product>(_stock.Values);
        }
    }
}
