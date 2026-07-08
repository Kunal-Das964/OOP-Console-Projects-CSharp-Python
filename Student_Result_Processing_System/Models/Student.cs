using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Models
{
    public class Student
    {
        public string name { get; private set; }
        public string rollNo { get; private set; }
        public List<int> marks { get; private set; }

        public Student(string name, string rollNo, List<int> marks)
        {
            this.name = name;
            this.rollNo = rollNo;
            this.marks = marks;
        }

        public int GetTotalMarks()
        {
            int total = 0;
            foreach(int m in marks)
            {
                total += m;
            }
            return total;
        }

        public double GetAverage()
        {
            return GetTotalMarks() / marks.Count;
        }

        public string getGrade()
        {
            int avg = (int)GetAverage();

            if (avg >= 90) return "A";
            else if (avg >= 75) return "B";
            else if (avg >= 60) return "C";
            else if (avg >= 35) return "D";
            else return "F";
        }


        public bool isPass()
        {
            //if (marks.Count == 0)
            //{
            //    Console.WriteLine("No marks are given.");
            //    return false;
            //}

            foreach(int m in marks)
            {
                if(m < 35)
                {
                    return false;
                }
            }

            return true;
        }

        public void DisplayInfo()
        {
            string status = isPass() ? "Pass" : "Fail";
            Console.WriteLine($"Roll No: {rollNo} | Name: {name} | Marks: [{string.Join(", ", marks)}]" + 
                $"Average: {GetAverage():F2} | Grade: {getGrade()} | Status: {status}");
        }

        public string GenerateReportCardText()
        {
            string status = isPass() ? "Pass" : "Fail";
            var lines = new List<string>();
            lines.Add("--- Report Card ---");
            lines.Add($"Name: {name}");
            lines.Add($"Roll No: {rollNo}");
            lines.Add($"Subject 1 Marks: {marks[0]}");
            lines.Add($"Subject 2 Marks: {marks[1]}");
            lines.Add($"Subject 3 Marks: {marks[2]}");
            lines.Add($"Total Marks: {GetTotalMarks()}");
            lines.Add($"Average: {GetAverage():F2}");
            lines.Add($"Grade: {getGrade()}");
            lines.Add($"Status: {status}");
            return string.Join("\n", lines);
        }
    }
}
