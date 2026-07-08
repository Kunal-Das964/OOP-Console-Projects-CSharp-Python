import os

def extract_error_entries(entries):
    results = []
    for e in entries:
        if e._level == "ERROR":
            results.append(e)
    return results

def write_errors_to_file(error_entries, filepath):
    os.makedirs(os.path.dirname(filepath), exist_ok=True)
    with open(filepath, "w") as f:
        for e in error_entries:
            f.write(f"{e._timestamp} | {e._level} | {e._message}\n")