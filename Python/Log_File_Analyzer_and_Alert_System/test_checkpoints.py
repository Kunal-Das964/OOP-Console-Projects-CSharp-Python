from models.log_entry import LogEntry
from Python.Log_File_Analyzer_and_Alert_System.services.log_loader import load_log_entries
from Python.Log_File_Analyzer_and_Alert_System.services.level_counter import count_log_levels, print_level_counts
from Python.Log_File_Analyzer_and_Alert_System.services.error_extractor import extract_error_entries, write_errors_to_file
import os
from Python.Log_File_Analyzer_and_Alert_System.services.sliding_window import check_sliding_window_alerts, print_sliding_window_alerts
from Python.Log_File_Analyzer_and_Alert_System.services.word_frequency import print_most_frequent_error_word


def FilePath(name):
    BASEPATH = os.path.dirname(os.path.abspath(__file__))
    return os.path.join(BASEPATH, "data", name)


# entries = load_log_entries(FilePath("app.log"))
# print(f"Loaded {len(entries)} valid entries.")
# for e in entries:
#     e.display_info()
#     print(repr(e._level))  # check for hidden leading/trailing spaces

# counts = count_log_levels(entries)
# print_level_counts(counts)

# error_entries = extract_error_entries(entries)
# print(f"Found {len(error_entries)} ERROR entries.")
# for e in error_entries:
#     e.display_info()

# write_errors_to_file(error_entries, FilePath("errors_only.log"))

# test_entries = [
#     LogEntry("t1", "INFO", "m1"),
#     LogEntry("t2", "ERROR", "m2"),
#     LogEntry("t3", "ERROR", "m3"),
#     LogEntry("t4", "WARN", "m4"),
#     LogEntry("t5", "ERROR", "m5"),
#     LogEntry("t6", "INFO", "m6"),
# ]

# alerts = check_sliding_window_alerts(test_entries)
# print_sliding_window_alerts(alerts)


test_entries = [
    LogEntry("t1", "ERROR", "Connection timeout occurred"),
    LogEntry("t2", "ERROR", "Database timeout during query"),
    LogEntry("t3", "INFO", "User logged in"),
    LogEntry("t4", "ERROR", "Timeout while connecting"),
]

print_most_frequent_error_word(test_entries)
