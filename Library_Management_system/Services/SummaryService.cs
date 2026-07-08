using Microsoft.Win32.SafeHandles;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SummaryService
    {
        public static void PrintSummary(Library library)
        {
            int total = library.GetTotalBooks();
            int borrowed = library.GetCurrentlyBorrowedCount();
            List<Book> allbooks = library.GetAllBooks();

            List<Book> sortedBooks = SortHelper.Sorting(allbooks, "borrowcount", true);
            int topCount = Math.Min(3, sortedBooks.Count);

            List<Book> top3 = sortedBooks.GetRange(0, topCount);

            Console.WriteLine("--- Library Summary ---");
            Console.WriteLine($"Total Books: {total}");
            Console.WriteLine($"Currently Borrowed: {borrowed}");
            Console.WriteLine("Top 3 most borrowed: ");

            if(top3.Count == 0)
            {
                Console.WriteLine($" No books in Library");
            }
            else
            {
                for(int i = 0; i < top3.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {top3[i].Title} (borrowed) {top3[i].BorrowCount} times");

                }
            } 
        }
    }
}
