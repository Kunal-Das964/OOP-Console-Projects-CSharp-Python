import os
from models.student import Student

def load_students(filepath, error_log_path):
    students = []

    if not os.path.exists(filepath):
        print(f"Results file not found: {filepath}")
        return students

    os.makedirs(os.path.dirname(error_log_path), exist_ok=True)

    with open(filepath, "r") as f, open(error_log_path, "w") as error_log:
        for raw_line in f:
            line = raw_line.strip()

            if not line:
                continue  # skip blank lines silently, not an error

            parts = line.split(",")

            if len(parts) != 5:
                error_log.write(f"Skipped (missing one or more fields): {line}\n")
                continue

            name, roll_no, mark1_str, mark2_str, mark3_str = parts

            marks = []
            conversion_failed = False
            for mark_str in [mark1_str, mark2_str, mark3_str]:
                try:
                    marks.append(float(mark_str))
                except ValueError:
                    conversion_failed = True
                    break

            if conversion_failed:
                error_log.write(f"Skipped (non-numeric mark '{mark_str}'): {line}\n")
                continue

            student = Student(name, roll_no, marks)
            students.append(student)

    return students