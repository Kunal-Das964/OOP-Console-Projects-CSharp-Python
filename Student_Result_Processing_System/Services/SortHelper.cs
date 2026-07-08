using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SortHelper
    {
        public static List<Student> quickSort(List<Student> students, int low, int high)
        {
            //List<Student> sortedStudents = new List<Student>(students);
            if(low < high)
            {
                int pivot = Patition(students, low, high);
                quickSort(students, low, pivot - 1);
                quickSort(students, pivot + 1, high);
            }

            return students;
        }

        public static int Patition(List<Student> students, int low, int high)
        {
            int idx = low - 1;
            for (int j = low; j < high; j++)
            {
                if (students[j].GetTotalMarks() > students[high].GetTotalMarks())
                {
                    idx++;
                    Student a = students[idx];
                    students[idx] = students[j];
                    students[j] = a;
                }
            }
            Student temp = students[idx + 1];
            students[idx + 1] = students[high];
            students[high] = temp;

            return idx + 1;
        }

        public static List<Student> SortStudentsByTotalMarks(List<Student> students)
        {
            List<Student> copyStudents = new List<Student>(students);
            return quickSort(copyStudents, 0, students.Count-1);   
        }
    }
}
