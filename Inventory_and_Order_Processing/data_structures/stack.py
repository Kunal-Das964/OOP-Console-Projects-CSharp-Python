class Stack:
    def __init__(self):
        self._items = []
        
    def push(self, item):
        self._items.append(item)
    
    def pop(self):
        if self.is_empty():
            print("Stack is empty. Nothing to pop.")
            return None
        return self._items.pop()
    
    def peek(self):
        if self.is_empty():
            print("Stack in empty. Nothing to peek")
            return None
        return self._items[-1]
    
    def is_empty(self):
        return len(self._items) == 0
