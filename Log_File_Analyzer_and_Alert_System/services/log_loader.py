import os
from models.log_entry import LogEntry

def load_log_entries(filepath):
    entries = []
    
    if not os.path.exists(filepath):
        print(f"Log file not found: {filepath}")
        return entries
    
    with open(filepath, "r") as f:
        for rawLine in f:
            line = rawLine.strip()
            
            if not line:
                continue
            
            parts = line.split("|")
            
            if len(parts) != 3:
                print(f"Skipped malformed line: {line}")
                continue
            
            timestamp = parts[0].strip()
            level = parts[1].strip()
            message = parts[2].strip()
            
            entry = LogEntry(timestamp, level, message)
            entries.append(entry)
            
    return entries