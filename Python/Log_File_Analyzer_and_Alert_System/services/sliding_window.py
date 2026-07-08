def check_sliding_window_alerts(entries, window_size=5, error_threshold=3):
    n = len(entries)
    alerts =[]
    
    if n < window_size:
        return alerts
    
    for start in range(n-window_size+1):
        window = entries[start:start + window_size]
        error_count = 0
        for e in window:
            if e._level == "ERROR":
                error_count += 1
        
        if error_count >= error_threshold:
            alerts.append((start, error_count))
            
    return alerts

def print_sliding_window_alerts(alerts):
    if len(alerts) == 0:
        print("No sliding window alerts triggered.")
        return
    print("--- Sliding Window Alerts ---")
    for s, ec in alerts:
        print(f"Alerts: {ec} ERROR entries found in window staring at line {s}.")
        