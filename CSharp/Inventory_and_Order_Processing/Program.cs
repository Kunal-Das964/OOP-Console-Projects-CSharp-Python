using DataStructures;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_and_Order_Processing
{
    public class Program
    {
        public static string GetFilePath(string name)
        {
            string BASEPATH = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Data"));
            return Path.Combine(BASEPATH, name);
        }

        private static void PrintMenu()
        {
            Console.WriteLine("\n--- Inventory Menu ---");
            Console.WriteLine("1. Place an order");
            Console.WriteLine("2. Undo last order");
            Console.WriteLine("3. Ship next pending order");
            Console.WriteLine("4. View low-stock alerts");
            Console.WriteLine("5. Find two products closest to a budget");
            Console.WriteLine("6. Exit");
        }

        private static void PlaceOrderFlow(Inventory inventory)
        {
            Console.Write("Customer name: ");
            string customerName = Console.ReadLine().Trim();

            List<(string, int)> items = new List<(string, int)>();

            Console.WriteLine("Enter items one at a time. Type 'done' as the product name to finish.");
            while (true)
            {
                Console.Write("Product name (or 'done'): ");
                string productName = Console.ReadLine().Trim();
                if (productName.ToLower() == "done")
                {
                    break;
                }

                Console.Write($"Quantity of '{productName}': ");
                if (!int.TryParse(Console.ReadLine().Trim(), out int quantity))
                {
                    Console.WriteLine("Invalid quantity. Must be a whole number. Item not added.");
                    continue;
                }

                items.Add((productName, quantity));
            }

            if (items.Count == 0)
            {
                Console.WriteLine("No items entered. Order cancelled.");
                return;
            }

            inventory.PlaceOrder(customerName, items);
        }

        private static void LowStockFlow(Inventory inventory)
        {
            List<Product> lowStock = inventory.GetLowStockProducts();
            if (lowStock.Count == 0)
            {
                Console.WriteLine("No low-stock items.");
                return;
            }
            Console.WriteLine("--- Low Stock Alerts ---");
            foreach (Product product in lowStock)
            {
                product.DisplayInfo();
            }
        }

        private static void BudgetFlow(Inventory inventory)
        {
            Console.Write("Enter your budget: ");
            if (!double.TryParse(Console.ReadLine().Trim(), out double budget))
            {
                Console.WriteLine("Invalid budget. Must be a number.");
                return;
            }

            List<Product> allProducts = inventory.GetAllProducts();
            ClosestPairResult result = BudgetFinder.FindClosestPairToBudget(allProducts, budget);

            if (result != null)
            {
                Console.WriteLine($"Closest pair: {result.ProductLeft.Name} + {result.ProductRight.Name} = " +
                                   $"{result.CombinedPrice} (difference from budget: {result.Difference})");
            }
        }
        static void Main(string[] args)
        {
            string stockFile = GetFilePath("stock.txt");
            string ordersFile = GetFilePath("orders.txt");

            Inventory inventory = new Inventory(ordersFile);
            inventory.LoadStock(stockFile);

            while (true)
            {
                PrintMenu();
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine().Trim();

                switch (choice)
                {
                    case "1":
                        PlaceOrderFlow(inventory);
                        break;
                    case "2":
                        inventory.UndoLastOrder();
                        break;
                    case "3":
                        inventory.ShipNextOrder();
                        break;
                    case "4":
                        LowStockFlow(inventory);
                        break;
                    case "5":
                        BudgetFlow(inventory);
                        break;
                    case "6":
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option, try again.");
                        break;
                }
            }
        }
    }
}
