class Queue:
    def __init__(self):
        self._items = []
        
    def enqueue(self, item):
        self._items.append(item)
        
    def dequeue(self):
        if self.is_empty():
            print("Queue is empty. Nothing to dequeue")
            return None
        return self._items.pop(0)
    
    def peek(self):
        if self.is_empty():
            print("Queue is empty. Nothing to peek")
            return None
        return self._items[0]
    
    def is_empty(self):
        return len(self._items) == 0