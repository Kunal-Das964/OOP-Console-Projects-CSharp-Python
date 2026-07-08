from datetime import datetime

class Transaction:
    def __init__(self, account_number, transaction_type, amount):
        self.account_number = account_number
        self.transaction_type = transaction_type
        self.amount = amount
        self.timestamp = datetime.now()
        
    def display(self):
        print(f"[{self.timestamp}] {self.account_number} | {self.transaction_type} | {self.amount}")