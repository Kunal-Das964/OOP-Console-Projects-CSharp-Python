using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class LibraryStorage
    {
        public static void SaveLibrary(Library library, string filepath)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filepath));
            using (StreamWriter writer = new StreamWriter(filepath))
            {
                foreach(Book book in library.GetAllBooks())
                {
                    string bookType;
                    string extraField;

                    if(book is Ebook ebook)
                    {
                        bookType = "Ebook";
                        extraField = ebook.Filesize.ToString();
                    }
                    else if(book is PrintedBook printedbook)
                    {
                        bookType = "Printed";
                        extraField = printedbook.ShelfNumber.ToString();
                    }
                    else
                    {
                        continue;
                    }

                    string line = $"{bookType}, {book.Title}, {book.Author}, {book.Isbn}, {extraField}, {book.IsBorrowed}, {book.BorrowCount}";
                    writer.WriteLine(line);
                }
            }
            Console.WriteLine("Inside Save Library");
        }

        // loadLibrary
        public static Library LoadLibrary(string filepath)
        {
            Library library = new Library();

            if (!File.Exists(filepath))
            {
                return library;
            }

            string[] lines = File.ReadAllLines(filepath);

            for (int i = 0; i < lines.Length; i++)
            {
                int lineNumber = i + 1;
                string line = lines[i].Trim();

                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                string[] fields = line.Split(',');

                if(fields.Length != 7)
                {
                    Console.WriteLine($"Skipping corrupt line {lineNumber}: expected 7 fields, found {fields.Length}.");
                    continue;
                }

                string bookType = fields[0].Trim();
                string title = fields[1].Trim();
                string author = fields[2].Trim();
                string isbn = fields[3].Trim();
                string extraField = fields[4].Trim();
                string isBorrowedStr = fields[5].Trim();
                string borrowedCountStr = fields[6].Trim();

                try
                {
                    Book book;

                    if(bookType == "Ebook")
                    {
                        double filesize = double.Parse(extraField);
                        book = new Ebook(title, author, isbn, filesize);
                    }
                    else if(bookType == "Printed")
                    {
                        int shelfNumber = int.Parse(extraField);
                        book = new PrintedBook(title, author, isbn, shelfNumber);
                    }
                    else
                    {
                        Console.WriteLine($"Skipping corrupt line {lineNumber}: unknown type tag '{bookType}'.");
                        continue;
                    }

                    book.IsBorrowed = bool.Parse(isBorrowedStr);
                    book.BorrowCount = int.Parse(borrowedCountStr);

                    library.AddBook(book);
                }
                catch(FormatException ex)
                {
                    Console.WriteLine($"Skipping corrupt line {lineNumber}: {ex.Message}");
                    continue;
                }
            }

            return library;
        }
    }
}
