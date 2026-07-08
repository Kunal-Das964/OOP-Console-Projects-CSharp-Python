import os

def write_payslips(employees, filepath):
    os.makedirs(os.path.dirname(filepath), exist_ok=True)

    with open(filepath, "w") as f:
        for employee in employees:
            payslip_text = employee.generate_payslip_text()
            f.write(payslip_text)
            f.write("\n" + ("=" * 40) + "\n")