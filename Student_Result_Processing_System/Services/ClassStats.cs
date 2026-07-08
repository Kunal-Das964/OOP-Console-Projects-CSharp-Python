using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class ClassStats
    {
        public static Student GetClassTopper(List<Student> students)
        {
            if(students.Count == 0)
            {
                return null;
            }
            List<Student> ranked = SortHelper.SortStudentsByTotalMarks(students);
            return ranked[0];
        }

        public static double GetClassAverage(List<Student> students)
        {
            if (students.Count == 0)
            {
                return 0;
            }

            double total = 0;
            foreach(Student s in students)
            {
                total += s.GetAverage();
            }
            return total / students.Count;
        }

        public static double GetPassPercentage(List<Student> students)
        {
            if (students.Count == 0)
            {
                return 0;
            }

            int passCount = 0;
            foreach(Student s in students)
            {
                if (s.isPass())
                {
                    passCount++;
                }
            }

            return ((double)passCount / students.Count) * 100;
        }

        public static void PrintSummary(List<Student> students)
        {
            Student topper = GetClassTopper(students);
            double average = GetClassAverage(students);
            double passPct = GetPassPercentage(students);

            Console.WriteLine("--- Class Summary ---");
            if(topper == null)
            {
                Console.WriteLine("NO students to summarize");
                return;
            }

            Console.WriteLine($"Class Topper: {topper.name} (Roll No: {topper.rollNo}, Total Marks: {topper.GetTotalMarks()})");
            Console.WriteLine($"Class Average: {average:F2}");
            Console.WriteLine($"Pass Percentage: {passPct:F2}%");
        }
    }
}
