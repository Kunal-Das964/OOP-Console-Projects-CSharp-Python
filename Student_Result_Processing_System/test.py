import os
from models.student import Student
from Python.Student_Result_Processing_System.services.result_loader import load_students
from Python.Student_Result_Processing_System.services.sorting import sort_students_by_total_marks
from Python.Student_Result_Processing_System.services.class_stats import print_class_summary
from Python.Student_Result_Processing_System.services.report_card import write_report_cards





def get_filePath(name):
    BasePath = os.path.dirname(os.path.abspath(__file__))
    return os.path.join(BasePath, "data", name)

students = load_students(, )



write_report_cards(students, )