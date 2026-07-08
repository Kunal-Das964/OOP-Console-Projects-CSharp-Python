from models.employee import Employee, Manager
import os
from Python.Employee_and_Payroll_Manager.services.payroll_loader import load_employees
from models.employee import Manager
from Python.Employee_and_Payroll_Manager.services.sorting import sort_employees_by_salary
from Python.Employee_and_Payroll_Manager.services.grouping import average_salary_by_department, print_department_averages
from Python.Employee_and_Payroll_Manager.services.payslip import write_payslips


# emp1 = Employee("Alice", "E1", 50000, "Engineering")
# mgr1 = Manager("Bob", "M1", 70000, "Engineering", 5, 0.10)

# employees = [emp1, mgr1]
# for e in employees:
#     e.display_info()




employees = load_employees(, )

# employees.append(Manager("Frank", "M1", 60000, "Engineering", 4, 0.20))  
# employees.append(Manager("Grace", "M2", 40000, "Sales", 3, 0.05))        

# sorted_desc = sort_employees_by_salary(employees)
# print("--- Ranked by salary (highest first) ---")
# for e in sorted_desc:
#     e.display_info()


# averages = average_salary_by_department(employees)
# print_department_averages(averages)


write_payslips(employees, )