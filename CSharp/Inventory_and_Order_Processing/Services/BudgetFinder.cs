using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ClosestPairResult
    {
        public Product ProductLeft { get; set; }
        public Product ProductRight { get; set; }
        public double CombinedPrice { get; set; }
        public double Difference { get; set; }
    }
    public static class BudgetFinder
    {
        public static ClosestPairResult FindClosestPairToBudget(List<Product> products, double budget)
        {
            if (products.Count < 2)
            {
                Console.WriteLine("Not enough products to form a pair.");
                return null;
            }

            List<Product> sortedProducts = SortHelper.SortProductsByPrice(products);

            int left = 0;
            int right = sortedProducts.Count - 1;

            ClosestPairResult bestPair = null;
            double bestDifference = double.PositiveInfinity;

            while (left < right)
            {
                Product productLeft = sortedProducts[left];
                Product productRight = sortedProducts[right];
                double combinedPrice = productLeft.Price + productRight.Price;
                double difference = Math.Abs(combinedPrice - budget);

                if (difference < bestDifference)
                {
                    bestDifference = difference;
                    bestPair = new ClosestPairResult
                    {
                        ProductLeft = productLeft,
                        ProductRight = productRight,
                        CombinedPrice = combinedPrice,
                        Difference = difference
                    };
                }

                if (combinedPrice < budget)
                {
                    left++;
                }
                else if (combinedPrice > budget)
                {
                    right--;
                }
                else
                {
                    break; 
                }
            }

            return bestPair;
        }
    }
}
