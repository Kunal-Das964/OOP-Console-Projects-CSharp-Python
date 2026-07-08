from models.transaction import Transaction
from Services.storage import append_transaction

class Ledger:
    def __init__(self, filepath):
        self.accounts = {}
        self.transactions = []
        self.filepath = filepath
        
    def add_account(self, account):
        if account.account_number in self.accounts:
            print(f"Account {account.account_number} already exists.")
            return False
        self.accounts[account.account_number] = account
        return True
    
    def deposit(self, account_number, amount):
        if account_number not in self.accounts:
            print(f"No account found wiht number {account_number}")
            return
        account = self.accounts[account_number]
        account.deposit(amount)
        transaction = Transaction(account_number, "deposit", amount)
        self.transactions.append(transaction)
        append_transaction(transaction, self.filepath)
        
    def withdraw(self, account_number, amount):
        if account_number not in self.accounts:
            print(f"No account found wiht number {account_number}")
            return
        account = self.accounts[account_number]
        account.withdraw(amount)
        transaction = Transaction(account_number, "withdraw", amount)
        self.transactions.append(transaction)
        append_transaction(transaction, self.filepath)
        
    def apply_monthly_update_all(self):
        for account in self.accounts.values():
            account.apply_monthly_update()
            
    def get_all_transactions(self):
        return self.transactions
    
    def get_account(self, account_number):
        return self.accounts.get(account_number)