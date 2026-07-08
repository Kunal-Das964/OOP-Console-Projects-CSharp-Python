import os
from models.employee import Employee

def load_employees(filepath, error_log_path):
    employees = []

    if not os.path.exists(filepath):
        print(f"Employee file not found: {filepath}")
        return employees

    os.makedirs(os.path.dirname(error_log_path), exist_ok=True)

    with open(filepath, "r") as f, open(error_log_path, "w") as error_log:
        for raw_line in f:
            line = raw_line.strip()

            if not line:
                continue 

            parts = line.split(",")

            if len(parts) != 4:
                error_log.write(f"Skipped (wrong number of fields): {line}\n")
                continue

            name, emp_id, salary_str, department = parts

            try:
                salary = float(salary_str)
            except ValueError:
                error_log.write(f"Skipped (invalid salary '{salary_str}'): {line}\n")
                continue

            employee = Employee(name, emp_id, salary, department)
            employees.append(employee)

    return employees