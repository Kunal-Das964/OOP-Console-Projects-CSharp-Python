using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Library
    {
        private Dictionary<string, Book> books = new Dictionary<string, Book>();

        // Add Book
        public bool AddBook(Book book)
        {
            if (books.ContainsKey(book.Isbn))
            {
                Console.WriteLine($"Book with ISBN {book.Isbn} already exists.");
                return false;
            }
            books[book.Isbn] = book;
            //Console.WriteLine($"\nBook {book.Title} added to the library.");
            return true;
        }

        // Borrow Book
        public bool BorrowBook(string isbn)
        {
            if (!books.ContainsKey(isbn))
            {
                Console.WriteLine($"No book found with ISBN {isbn}.");
                return false;
            }
            Book book = books[isbn];
            if (book.IsBorrowed)
            {
                Console.WriteLine($"Book '{book.Title}' is already borrowed.");
                return false;
            }
            book.IsBorrowed = true;
            book.BorrowCount++;
            Console.WriteLine($"\nBook '{book.Title}' is borrowed.");
            return true;
        }

        // Return Book
        public bool ReturnBook(string isbn)
        {
            if (!books.ContainsKey(isbn))
            {
                Console.WriteLine($"No book found with ISBN {isbn}.");
                return false;
            }
            Book book = books[isbn];
            if (!book.IsBorrowed)
            {
                Console.WriteLine($"Book {book.Title} was not borrowed.");
                return false;
            }
            book.IsBorrowed = false;
            Console.WriteLine($"Book '{book.Title}' is returned.");
            return true;
        }

        // Search by title
        public List<Book> SearchByTitle(string title)
        {
            List<Book> result = new List<Book>();
            foreach(Book book in books.Values)
            {
                if (book.Title.ToLower().Contains(title.ToLower()))
                {
                    result.Add(book);
                }
            }
            return result;
        }

        // Get all books
        public List<Book> GetAllBooks()
        {
            return new List<Book>(books.Values);
        }

        // Get total books
        public int GetTotalBooks()
        {
            return books.Count;
        }

        // Get currently borrowed book
        public int GetCurrentlyBorrowedCount()
        {
            int count = 0;
            foreach(Book book in books.Values)
            {
                if (book.IsBorrowed)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
