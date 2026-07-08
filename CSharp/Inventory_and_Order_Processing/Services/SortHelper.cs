using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SortHelper
    {
        //public static List<Product> QuickSort(List<Product> products, int low, int high)
        //{
        //    if(low < high)
        //    {
        //        int pivot = Patition(products, low, high);
        //        QuickSort(products, low, pivot - 1);
        //        QuickSort(products, pivot + 1, high);
        //    }
        //    return products;
        //}

        //public static int Patition(List<Product> products, int low, int high)
        //{
        //    int idx = low - 1;
        //    for(int j = low; j < high; j++)
        //    {
        //        if (products[j].GetPrice() > products[high].GetPrice())
        //        {
        //            idx++;
        //            Product a = products[idx];
        //            products[idx] = products[j];
        //            products[j] = a;
        //        }
        //    }
        //    Product temp = products[idx + 1];
        //    products[idx + 1] = products[high];
        //    products[high] = temp;

        //    return idx + 1;
        //}

        public static List<Product> Quicksort(List<Product> products)
        {
            // Step 5: base case — 0 or 1 elements are already sorted
            if (products.Count <= 1)
            {
                return products;
            }

            // Step 6: choose the last element as the pivot
            Product pivot = products[products.Count - 1];
            double pivotPrice = pivot.Price;

            // Step 7: partition the rest into two lists
            List<Product> lessThan = new List<Product>();
            List<Product> greaterEqual = new List<Product>();

            for (int i = 0; i < products.Count - 1; i++) // every element except the pivot itself
            {
                if (products[i].Price < pivotPrice)
                {
                    lessThan.Add(products[i]);
                }
                else
                {
                    greaterEqual.Add(products[i]);
                }
            }

            // Step 8 + 9: recursively sort both sides, then combine with pivot in the middle
            List<Product> sortedLeft = Quicksort(lessThan);
            List<Product> sortedRight = Quicksort(greaterEqual);

            List<Product> result = new List<Product>();
            result.AddRange(sortedLeft);
            result.Add(pivot);
            result.AddRange(sortedRight);

            return result;
        }

        public static List<Product> SortProductsByPrice(List<Product> products)
        {
            //List<Product> sortedProducts = new List<Product>(products);
            //QuickSort(sortedProducts, 0, sortedProducts.Count - 1);

            return Quicksort(products);
        }
    }
}
