using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class PayslipWriter
    {
        public static void WritePayslips(List<Employee> employees, string filepath)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filepath));

            using(StreamWriter writer = new StreamWriter(filepath, false))
            {
                foreach(Employee employee in employees)
                {
                    string payslipText = employee.GeneratePayslipText();
                    writer.WriteLine(payslipText);
                    writer.WriteLine(new string('=', 40));
                }
            }
        }
    }
}
