using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Ebook : Book
    {
        public double Filesize { get; private set; }
        public Ebook(string title, string author, string isbn, double filesize)
            : base(title, author, isbn)
        {
            Filesize = filesize;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Type: Ebook | Filesize: {Filesize} MB");
        }
    }
}
