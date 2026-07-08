using System;

namespace Models
{
    public class Book
    {
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string Isbn { get; private set; }
        public bool IsBorrowed { get; set; }
        public int BorrowCount { get; set; }

        public Book(string title, string author, string isbn)
        {
            Title = title;
            Author = author;
            Isbn = isbn;
            IsBorrowed = false;
            BorrowCount = 0;
        }
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Title: {Title} | Author: {Author} | ISBN: {Isbn}");
        }
    }
}
