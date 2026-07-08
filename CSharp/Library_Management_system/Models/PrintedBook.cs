using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PrintedBook : Book
    {
        public int ShelfNumber { get; private set; }

        public PrintedBook(string title, string author, string isbn, int shelfnumber)
            : base(title, author, isbn)
        {
            ShelfNumber = shelfnumber;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Type: PrintedBook | Shelf: {ShelfNumber}");
        }
    }
}
