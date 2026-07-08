using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class PayrollLoader
    {
        public static List<Employee> LoadEmployees(string filepath, string errorLogPath)
        {
            List<Employee> employees = new List<Employee>();

            if (!File.Exists(filepath))
            {
                Console.WriteLine($"Employee file not found: {filepath}");
                return employees;
            }

            Directory.CreateDirectory(Path.GetDirectoryName(errorLogPath));

            using (StreamWriter errorLog = new StreamWriter(errorLogPath, false)) // false = overwrite
            {
                string[] lines = File.ReadAllLines(filepath);
                foreach(string rawLine in lines)
                {
                    string line = rawLine.Trim();

                    if (string.IsNullOrEmpty(line))
                    {
                        continue; // skip blank line silently
                    }

                    string[] parts = line.Split(',');

                    if(parts.Length != 4)
                    {
                        errorLog.WriteLine($"Skipped (wrong number of fields): {line}");
                        continue;
                    }

                    string name = parts[0];
                    string empId = parts[1];
                    string salaryStr = parts[2];
                    string department = parts[3];

                    if(!double.TryParse(salaryStr, out double salary))
                    {
                        errorLog.WriteLine($"Skipped (invalid salary '{salaryStr}'): {line}");
                        continue;
                    }

                    Employee employee = new Employee(name, empId, salary, department);
                    employees.Add(employee);
                }
            }
            return employees;
        }
    }
}
