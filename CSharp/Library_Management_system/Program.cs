using Models;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_system
{
    public class Program
    {
        private static string GetLibraryFilePath()
        {
            string dataFolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Data"));
            return Path.Combine(dataFolder, "library.txt");

            //string filepath = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName, "Data", "library.txt");
            //return filepath;

        }

        private static void PrintMenu()
        {
            Console.WriteLine("\n--- Library Menu ---");
            Console.WriteLine("1. Add a book");
            Console.WriteLine("2. Borrow a book");
            Console.WriteLine("3. Return a book");
            Console.WriteLine("4. Search by Title");
            Console.WriteLine("5. Sort and list books");
            Console.WriteLine("6. Show borrowing summary");
            Console.WriteLine("7. Save and Exit");
        }

        private static void AddBookFlow(Library library)
        {
            Console.Write("Type (1=Ebook, 2=Printed): ");
            string bookType = Console.ReadLine().Trim();

            Console.Write("Title: ");
            string title = Console.ReadLine().Trim();
            Console.Write("Author: ");
            string author = Console.ReadLine().Trim();
            Console.Write("ISBN: ");
            string isbn = Console.ReadLine().Trim();

            Book book;

            if(bookType == "1")
            {
                Console.Write("Filesize (MB): ");
                if(!double.TryParse(Console.ReadLine().Trim(), out double filesize))
                {
                    Console.WriteLine("Invalid filesize. Must be a number.");
                    return;
                }
                book = new Ebook(title, author, isbn, filesize);
            }
            else if (bookType == "2")
            {
                Console.Write("Shelf number: ");
                if(!int.TryParse(Console.ReadLine().Trim(), out int shelfNumber))
                {
                    Console.WriteLine("Invalid shelf number. Must be a whole number.");
                    return;
                }
                book = new PrintedBook(title, author, isbn, shelfNumber);
            }
            else
            {
                Console.WriteLine("Invalid type selected.");
                return;
            }
            library.AddBook(book);

        }

        private static void BorrowFlow(Library library)
        {
            Console.Write("ISBN to borrow: ");
            string isbn = Console.ReadLine().Trim();
            library.BorrowBook(isbn);
        }

        private static void ReturnFlow(Library library)
        {
            Console.Write("ISBN to return: ");
            string isbn = Console.ReadLine().Trim();
            library.ReturnBook(isbn);
        }

        private static void SearchFlow(Library library)
        {
            Console.Write("Title to search: ");
            string title = Console.ReadLine().Trim();
            List<Book> result = library.SearchByTitle(title);

            if(result.Count == 0)
            {
                Console.WriteLine("No matching books found.");
            }
            foreach(Book book in result)
            {
                book.DisplayInfo();
            }
        }

        private static void SortFlow(Library library)
        {
            Console.Write("Sort by (title/filesize): ");
            string sortBy = Console.ReadLine().Trim();

            Console.Write("Descending? (y/n): ");
            bool desc = Console.ReadLine().Trim().ToLower() == "y";

            List<Book> allBooks = library.GetAllBooks();
            List<Book> sortedBooks = SortHelper.Sorting(allBooks, sortBy, desc);

            foreach(Book book in sortedBooks)
            {
                book.DisplayInfo();
            }
        }

        public static void Main(string[] args)
        {
            //Library library = new Library();
            string filePath = GetLibraryFilePath();
            Library library = LibraryStorage.LoadLibrary(filePath);

            while (true)
            {
                PrintMenu();
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine().Trim();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        AddBookFlow(library);
                        break;
                    case "2":
                        Console.Clear();
                        BorrowFlow(library);
                        break;
                    case "3":
                        Console.Clear();
                        ReturnFlow(library);
                        break;
                    case "4":
                        Console.Clear();
                        SearchFlow(library);
                        break;
                    case "5":
                        Console.Clear();
                        SortFlow(library);
                        break;
                    case "6":
                        Console.Clear();
                        SummaryService.PrintSummary(library);
                        break;
                    case "7":
                        Console.Clear();
                        LibraryStorage.SaveLibrary(library, filePath);
                        Console.WriteLine("Library saved. GoodBye!");
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid option, try again.");
                        break;
                }
            }
        }
    }
}
