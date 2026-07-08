using Models;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_and_Payroll_Manager
{
    public class Program
    {
        public static string GetDataPath(string filename)
        {
            string datafolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Data"));
            return Path.Combine(datafolder, filename);
        }

        static void Main(string[] args)
        {
            string employeeFile = GetDataPath("employees.txt");
            string errorFile = GetDataPath("Error.Log");
            string payslipsFile = GetDataPath("payslips.txt");

            // Load employees from file
            List<Employee> employees = PayrollLoader.LoadEmployees(employeeFile, errorFile);
            Console.WriteLine($"Loaded {employees.Count} employees from file.");

            // Manually add manager, since the file format has no columns for team size/bonus percent
            employees.Add(new Manager("Frank", "M1", 60000, "Engineering", 4, 0.20));
            employees.Add(new Manager("Grace", "M2", 40000, "Sales", 3, 0.05));
            Console.WriteLine("Added 2 manually-created Manager records (not from file)");

            // Sort by actual calculated salary, descending
            List<Employee> sortedEmployees = SortHelper.SortEmployeesBySalary(employees);
            Console.WriteLine("\n--- Employees Ranked by Salary ---");
            foreach(Employee e in sortedEmployees)
            {
                e.DisplayInfo();
            }


            // Department wise average salary
            Dictionary<string, double> averages = GroupingHelper.AverageSalaryByDepartment(employees);
            Console.WriteLine();
            GroupingHelper.PrintDepartmentAverages(averages);

            // Payslip for every employee
            PayslipWriter.WritePayslips(employees, payslipsFile);

            // final summary
            Console.WriteLine($"\nDone. Payslips written to {payslipsFile}. Check {errorFile} for any skipped rows.");

            Console.ReadLine();
        }
    }
}
