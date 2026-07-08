class Employee:
    def __init__(self, name, emp_id, base_salary, department):
        self._name = name
        self._id = emp_id
        self._base_salary = base_salary
        self._department = department

    def calculate_salary(self):
        return self._base_salary

    def display_info(self):
        print(f"ID: {self._id} | Name: {self._name} | Department: {self._department} | Salary: {self.calculate_salary()}")

    def generate_payslip_text(self):
        lines = []
        lines.append(f"Employee ID: {self._id}")
        lines.append(f"Name: {self._name}")
        lines.append(f"Department: {self._department}")
        lines.append(f"Base Salary: {self._base_salary}")
        lines.append(f"Final Salary: {self.calculate_salary()}")
        return "\n".join(lines)


class Manager(Employee):
    def __init__(self, name, emp_id, base_salary, department, team_size, bonus_percent):
        super().__init__(name, emp_id, base_salary, department)
        self._team_size = team_size
        self._bonus_percent = bonus_percent

    def calculate_salary(self):
        return self._base_salary + (self._base_salary * self._bonus_percent)

    def display_info(self):
        print(f"ID: {self._id} | Name: {self._name} | Department: {self._department} | "
              f"Team size: {self._team_size} | Salary (with bonus): {self.calculate_salary()}")

    def generate_payslip_text(self):
        base_text = super().generate_payslip_text()
        bonus_amount = self._base_salary * self._bonus_percent
        extra_lines = []
        extra_lines.append(f"Team Size: {self._team_size}")
        extra_lines.append(f"Bonus Percent: {self._bonus_percent * 100:.1f}%")
        extra_lines.append(f"Bonus Amount: {bonus_amount}")
        return base_text + "\n" + "\n".join(extra_lines)