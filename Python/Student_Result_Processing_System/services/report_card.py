import os

def write_report_cards(students, folder_path):
    os.makedirs(folder_path, exist_ok=True)

    for student in students:
        filename = f"{student._roll_no}.txt"
        filepath = os.path.join(folder_path, filename)

        with open(filepath, "w") as f:
            f.write(student.generate_report_card_text())