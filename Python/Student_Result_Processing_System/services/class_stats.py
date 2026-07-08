from services.sorting import sort_students_by_total_marks

def get_class_topper(students):
    if len(students) == 0:
        return None
    ranked = sort_students_by_total_marks(students)
    return ranked[0]

def get_class_average(students):
    if len(students) == 0:
        return 0
    total = 0
    for student in students:
        total += student.get_average()
    return total / len(students)

def get_pass_percentage(students):
    if len(students) == 0:
        return 0
    pass_count = 0
    for student in students:
        if student.is_pass():
            pass_count += 1
    return (pass_count / len(students)) * 100

def print_class_summary(students):
    topper = get_class_topper(students)
    average = get_class_average(students)
    pass_pct = get_pass_percentage(students)

    print("--- Class Summary ---")
    if topper is None:
        print("No students to summarize.")
        return

    print(f"Class Topper: {topper._name} (Roll No: {topper._roll_no}, Total Marks: {topper.get_total_marks()})")
    print(f"Class Average: {average:.2f}")
    print(f"Pass Percentage: {pass_pct:.2f}%")