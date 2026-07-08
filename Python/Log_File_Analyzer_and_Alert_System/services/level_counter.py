def count_log_levels(entries):
    counts = {}
    
    for e in entries:
        level = e._level
        
        if level not in counts:
            counts[level] = 1
        else:
            counts[level] += 1
            
    return counts

def print_level_counts(counts):
    print("--- Log Level Frequency ---")
    for level, count in counts.items():
        print(f"{level}: {count}")