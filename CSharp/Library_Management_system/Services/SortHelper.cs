using Models;
using System;
using System.Collections.Generic;


namespace Services
{
    public static class SortHelper
    {
        public static List<Book> Sorting(List<Book> books, string sortBy="title", bool desc = false)
        {
            List<Book> sortedBooks = new List<Book>(books);
            int n = sortedBooks.Count;

            for(int i = 0; i < n-1; i++)
            {
                bool swap = false;
                for(int j = 0; j < n-1-i; j++)
                {
                    Book first = sortedBooks[j];
                    Book second = sortedBooks[j + 1];

                    int comparison = CompareBooks(first, second, sortBy);
                    bool swapped = desc ? comparison < 0 : comparison > 0;

                    if (swapped)
                    {
                        Book temp = sortedBooks[j];
                        sortedBooks[j] = sortedBooks[j + 1];
                        sortedBooks[j + 1] = temp;
                        swap = true;
                    }
                }
                if (!swap)
                {
                    break;
                }
            }
            return sortedBooks;
        }

        public static int CompareBooks(Book first, Book second, string sortBy)
        {
            switch (sortBy.ToLower())
            {
                case "title":
                    return string.Compare(first.Title, second.Title, StringComparison.OrdinalIgnoreCase);
                case "filesize":
                    double size1 = (first is Ebook e1) ? e1.Filesize : 0;
                    double size2 = (second is Ebook e2) ? e2.Filesize : 0;
                    return size1.CompareTo(size2);
                case "borrowcount":
                    return first.BorrowCount.CompareTo(second.BorrowCount);
                default:
                    throw new ArgumentException($"Unknown sortBy field: {sortBy}");
            }
        }
    }
}
