from exceptions import InvalidAmountError, InsufficientFundsError

#--------------------Base Account---------------------------------
class Account:
    def __init__(self, account_number, initial_balance=0):
        self.account_number = account_number
        self.balance = initial_balance
        
    def deposit(self, amount):
        if amount < 0:
            raise InvalidAmountError("Deposit amount must be positive.")
        self.balance += amount
        
    def withdraw(self, amount):
        if amount <= 0:
            raise InvalidAmountError("Withdraw amount must to positive.")
        if amount > self.balance:
            raise InsufficientFundsError("Insufficient funds for this withdrawal.")
        self.balance -= amount
        
    def show_balance(self):
        print(f"Account: {self.account_number}\nBalance: {self.balance}")
        
    def apply_monthly_update(self):
        print(f"Account {self.account_number}: no monthly update defined.")
        
        
        
#-------------------Saving Account----------------------------------
class SavingsAccount(Account):
    def __init__(self, account_number, initial_balance=0, interest_rate=0.0):
        super().__init__(account_number, initial_balance)
        self.interest_rate = interest_rate
        
    def apply_interest(self):
        interest = self.balance * self.interest_rate
        self.balance += interest
        print(f"Account {self.account_number}: interest of {interest} applied.")
        
    def apply_monthly_update(self):
        self.apply_interest()
         

#------------------Current Account---------------------------------------
class CurrentAccount(Account):
    def __init__(self, account_number, initial_balance=0, overdraft_limit=0):
        super().__init__(account_number, initial_balance)
        self.overdraft_limit = overdraft_limit
        
    def withdraw(self, amount):
        if amount < 0:
            raise InvalidAmountError("Withdrawal amount must be positive.")
        if amount > self.balance + self.overdraft_limit:
            raise InsufficientFundsError("Withdrawal exceeds available balance and overdraft limit")
        self.balance -= amount
        
    def apply_monthly_update(self):
        print(f"Account {self.account_number}: current account maintenance check complete.")