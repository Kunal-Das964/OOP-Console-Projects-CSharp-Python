using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class ReportCardWriter
    {
        public static void WriteReportCards(List<Student> students, string folderPath)
        {
            Directory.CreateDirectory(folderPath);

            foreach(Student s in students)
            {
                string filename = $"{s.rollNo}.txt";
                string filepath = Path.Combine(folderPath, filename);

                File.WriteAllText(filepath, s.GenerateReportCardText());
            }
        }
    }
}
