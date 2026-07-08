class Student:
    def __init__(self, name, roll_no, marks):
        self._name = name
        self._roll_no = roll_no
        self._marks = marks  # list of 3 numbers

    def is_pass(self):
        for mark in self._marks:
            if mark < 35:
                return False
        return True

    def get_total_marks(self):
        total = 0
        for mark in self._marks:
            total += mark
        return total

    def get_average(self):
        return self.get_total_marks() / len(self._marks)

    def get_grade(self):
        average = self.get_average()
        if average >= 90:
            return "A"
        elif average >= 75:
            return "B"
        elif average >= 60:
            return "C"
        elif average >= 35:
            return "D"
        else:
            return "F"

    def display_info(self):
        status = "Pass" if self.is_pass() else "Fail"
        print(f"Roll No: {self._roll_no} | Name: {self._name} | Marks: {self._marks} | "
              f"Total: {self.get_total_marks()} | Average: {self.get_average():.2f} | "
              f"Grade: {self.get_grade()} | Status: {status}")
        
    def generate_report_card_text(self):
        status = "Pass" if self.is_pass() else "Fail"
        lines = []
        lines.append("--- Report Card ---")
        lines.append(f"Name: {self._name}")
        lines.append(f"Roll No: {self._roll_no}")
        lines.append(f"Subject 1 Marks: {self._marks[0]}")
        lines.append(f"Subject 2 Marks: {self._marks[1]}")
        lines.append(f"Subject 3 Marks: {self._marks[2]}")
        lines.append(f"Total Marks: {self.get_total_marks()}")
        lines.append(f"Average: {self.get_average():.2f}")
        lines.append(f"Grade: {self.get_grade()}")
        lines.append(f"Status: {status}")
        return "\n".join(lines)