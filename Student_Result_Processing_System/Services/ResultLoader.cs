using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ResultLoader
    {
        public static List<Student> LoadStudents(string filepath, string errorLogPath)
        {
            List<Student> students = new List<Student>();

            if (!File.Exists(filepath))
            {
                Console.WriteLine($"Results file not found: {filepath}");
                return students;
            }

            Directory.CreateDirectory(Path.GetDirectoryName(errorLogPath));
            using(StreamWriter errorLog = new StreamWriter(errorLogPath, false))
            {
                foreach(string rawLine in File.ReadAllLines(filepath))
                {
                    string line = rawLine.Trim();

                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }

                    string[] parts = line.Split(',');

                    if(parts.Length != 5)
                    {
                        errorLog.WriteLine($"Skipped (missing one or more fields): {line}");
                        continue;
                    }

                    string name = parts[0];
                    string rollNo = parts[1];
                    string[] marksStr = { parts[2], parts[3], parts[4] };

                    List<int> marks = new List<int>();
                    bool conversionFailed = false;
                    string failedMark = "";

                    foreach(string m in marksStr)
                    {
                        if(int.TryParse(m, out int mValue))
                        {
                            marks.Add(mValue);
                        }
                        else
                        {
                            conversionFailed = true;
                            failedMark = m;
                            break;
                        }
                    }

                    if (conversionFailed)
                    {
                        errorLog.WriteLine($"Skipped (non-numeric mark '{failedMark}'): {line}");
                        continue;
                    }

                    Student student = new Student(name, rollNo, marks);
                    students.Add(student);
                }
            }
            return students;
        }
    }
}
