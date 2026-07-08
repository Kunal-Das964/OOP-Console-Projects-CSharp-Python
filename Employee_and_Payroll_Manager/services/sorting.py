def sort_employees_by_salary(employees, desc=False):
    sorted_employees = employees.copy()  
    n = len(sorted_employees)

    for i in range(n):
        for j in range(0, n - i - 1):
            salary_j = sorted_employees[j].calculate_salary()
            salary_j1 = sorted_employees[j + 1].calculate_salary()

            if desc:
                should_swap = salary_j < salary_j1
            else:
                should_swap = salary_j > salary_j1

            if should_swap:
                sorted_employees[j], sorted_employees[j + 1] = sorted_employees[j + 1], sorted_employees[j]

    return sorted_employees