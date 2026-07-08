using Models;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Student_Result_Processing_System
{
    public class Program
    {
        private static string GetDataPath(string name)
        {
            string BASE_ADD = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Data");
            return Path.Combine(BASE_ADD, name);
        }
        static void Main(string[] args)
        {
            string resultsFile = GetDataPath("results.txt");
            string errorFile = GetDataPath("errors.log");
            string reportCardsFolder = GetDataPath("ReportCards");

            // Load students
            List<Student> students = ResultLoader.LoadStudents(resultsFile, errorFile);
            Console.WriteLine($"Loaded {students.Count} valid students.");

            // sorting
            List<Student> ranked = SortHelper.SortStudentsByTotalMarks(students);
            Console.WriteLine("\n--- Students Ranked by Total Marks ---");
            foreach (Student s in ranked)
            {
                s.DisplayInfo();
            }

            // Class topper, avg, passPct
            Console.WriteLine();
            ClassStats.PrintSummary(students);

            //write report card
            ReportCardWriter.WriteReportCards(students, reportCardsFolder);

            // Final summary
            Console.WriteLine($"\n Done. Report cards written to {reportCardsFolder}/. Check {errorFile} for any skipped rows.");

            Console.ReadLine();
        }
    }
}
