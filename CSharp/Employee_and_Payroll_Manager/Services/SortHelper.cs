using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class SortHelper
    {
        public static List<Employee> SortEmployeesBySalary(List<Employee> employees)
        {
            List<Employee> sortedEmployees = new List<Employee>(employees); // copy
            int n = sortedEmployees.Count;

            for(int i = 1; i<n; i++)
            {
                Employee key = sortedEmployees[i];
                double k = key.CalculateSalary();
                int j = i - 1;

                while(j>=0 && sortedEmployees[j].CalculateSalary() < k)
                {
                    sortedEmployees[j + 1] = sortedEmployees[j];
                    j--;
                }
                sortedEmployees[j + 1] = key;
            }
            return sortedEmployees;
        }
    }
}
