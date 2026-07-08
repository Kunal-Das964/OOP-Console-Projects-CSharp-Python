from services.log_loader import load_log_entries
from services.level_counter import count_log_levels, print_level_counts
from services.error_extractor import extract_error_entries, write_errors_to_file
from services.sliding_window import check_sliding_window_alerts, print_sliding_window_alerts
from services.word_frequency import print_most_frequent_error_word
import os

def FilePath(name):
    BASEPATH = os.path.dirname(os.path.abspath(__file__))
    return os.path.join(BASEPATH, "data", name)

def main():
    # Load log 
    entries = load_log_entries(FilePath("app.log"))
    print(f"Loaded {len(entries)} valid log entries.")
    
    # Log level
    print()
    counts = count_log_levels(entries)
    print_level_counts(counts)
    
    # Extract error entries
    error_entries = extract_error_entries(entries)
    write_errors_to_file(error_entries, FilePath("errors_only.log"))
    print(f"\nWrote {len(error_entries)} ERROR entries to {FilePath("errors_only.log")}")
    
    # Sliding window alerts
    print()
    alerts = check_sliding_window_alerts(entries)
    print_sliding_window_alerts(alerts)
    
    #Most frequent error word
    print()
    print_most_frequent_error_word(entries)
    
if __name__ == "__main__":
    main()