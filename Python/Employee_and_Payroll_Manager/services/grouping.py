def average_salary_by_department(employees):
    department_data = {} 

    for employee in employees:
        department = employee._department
        salary = employee.calculate_salary()

        if department not in department_data:
            department_data[department] = {"total": salary, "count": 1}
        else:
            department_data[department]["total"] += salary
            department_data[department]["count"] += 1

    averages = {}
    for department, data in department_data.items():
        averages[department] = data["total"] / data["count"]

    return averages


def print_department_averages(averages):
    print("--- Department-wise Average Salary ---")
    for department, avg in averages.items():
        print(f"{department}: {avg:.2f}")