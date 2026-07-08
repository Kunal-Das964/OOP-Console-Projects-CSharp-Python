import os
from models.employee import Manager
from services.payroll_loader import load_employees
from services.sorting import sort_employees_by_salary
from services.grouping import average_salary_by_department, print_department_averages
from services.payslip import write_payslips

def get_filePath(name):
    BasePath = os.path.dirname(os.path.abspath(__file__))
    return os.path.join(BasePath, "data", name)

def main():
    # Step 1: Load employees from file (Stage 2)
    employees = load_employees(get_filePath("employees.txt"), get_filePath("errors.log"))
    print(f"Loaded {len(employees)} employees from file.")

    # Step 2 (Step 6.4): manually add Managers, since the file format has no
    # columns for team_size/bonus_percent (Stage 2, Step 2.1 finding)
    employees.append(Manager("Frank", "M1", 60000, "Engineering", 4, 0.20))
    employees.append(Manager("Grace", "M2", 40000, "Sales", 3, 0.05))
    print("Added 2 manually-created Manager records (not from file).")

    # Step 3: Sort by actual calculated salary, descending (Stage 3)
    sorted_employees = sort_employees_by_salary(employees, desc=True)
    print("\n--- Employees Ranked by Salary ---")
    for e in sorted_employees:
        e.display_info()

    # Step 4: Department-wise average salary (Stage 4)
    averages = average_salary_by_department(employees)
    print()
    print_department_averages(averages)

    # Step 5: Write payslips for every employee (Stage 5)
    write_payslips(employees, get_filePath("payslips.txt"))

    # Step 6: Final summary
    print(f"\nDone. Payslips written to {get_filePath("payslips.txt")}. Check {get_filePath("errors.log")} for any skipped rows.")

if __name__ == "__main__":
    main()