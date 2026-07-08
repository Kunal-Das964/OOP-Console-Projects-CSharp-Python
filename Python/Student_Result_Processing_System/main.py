import os
from services.result_loader import load_students
from services.sorting import sort_students_by_total_marks
from services.class_stats import print_class_summary
from services.report_card import write_report_cards

def get_filePath(name):
    BasePath = os.path.dirname(os.path.abspath(__file__))
    return os.path.join(BasePath, "data", name)

def main():
    # Load students from file (Stage 2)
    students = load_students(get_filePath("results.txt"), get_filePath("errors.log"))
    print(f"Loaded {len(students)} valid students.")

    # Sort by total marks, descending (Stage 3)
    ranked = sort_students_by_total_marks(students)
    print("\n--- Students Ranked by Total Marks ---")
    for s in ranked:
        s.display_info()

    # Class topper, average, pass percentage (Stage 4)
    print()
    print_class_summary(students)

    # Write report cards (Stage 5)
    write_report_cards(students, get_filePath("reportcards"))

    # Final summary
    print(f"\nDone. Report cards written to {get_filePath("reportcards")}/. Check {get_filePath("errors.log")} for any skipped rows.")

if __name__ == "__main__":
    main()