class LogEntry:
    def __init__(self, timestamp, level, message):
        self._timestamp = timestamp
        self._level = level
        self._message = message
        
    def display_info(self):
        print(f"[{self._timestamp}] {self._level}: {self._message}")
    