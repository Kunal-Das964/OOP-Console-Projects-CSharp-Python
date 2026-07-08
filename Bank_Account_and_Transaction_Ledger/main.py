from Services.ledger import Ledger
from models.account import Account, SavingsAccount, CurrentAccount
from exceptions import InsufficientFundsError, InvalidAmountError
from Services.storage import load_transactions, print_statement
from Services.sorting import sort_transactions_by_amount, search_transaction_above
import os

BASE_DIR = os.path.dirname(os.path.abspath(__file__))
# LIBRARY_FILE = "data/library.txt"
TRANSACTIONS_FILE = os.path.join(BASE_DIR, "data", "transaction.txt")

def print_menu():
    print("--- Bank Menu ---")
    print("1. Create an account")
    print("2. Deposit")
    print("3. Withdraw")
    print("4. Show Balance")
    print("5. Apply monhtly update to all accounts")
    print("6. View statement (all or by account)")
    print("7. Sort transactions by amount")
    print("8. Search transactions above an amount")
    print("9. Exit")
    
def create_account_flow(ledger):
    acc_type = input("Type (1=Account, 2=Savings, 3=Current): ").strip()
    account_number = input("Account number: ").strip()
    
    try:
        initial_balance = float(input("Initial balance: ").strip())
    except ValueError:
        print("Invalid balance. Must be a number.")
        return
    
    if acc_type == "1":
        account = Account(account_number, initial_balance)
    elif acc_type == "2":
        try:
            interest_rate = float(input("Interest rate (e.g. 0.05 for 5%): ").strip())
        except ValueError:
            print("Invalid interest rate. Must be a number.")
            return
        account = SavingsAccount(account_number, initial_balance, interest_rate)
    elif acc_type == "3":
        try:
            overdraft_limit = float(input("Overdraft limit: ").strip())
        except ValueError:
            print("Invalid overdraft limit. Must be a number.")
            return
        account = CurrentAccount(account_number, initial_balance, overdraft_limit)
    else:
        print("Invalid type selected.")
        return
    
    ledger.add_account(account)
    
def deposit_flow(ledger):
    account_number = input("Account number: ").strip()
    try:
        amount = float(input("Amount to deposit: ").strip())
    except ValueError:
        print("Invalid amount. Must be a number")
        return
    
    try:
        ledger.deposit(account_number, amount)
        print("Deposit successful.")
    except InvalidAmountError as e:
        print(f"Deposit failed: {e}")
    
def withdraw_flow(ledger):
    account_number = input("Account number: ").strip()
    try:
        amount = float(input("Amount to withdraw: ").strip())
    except ValueError:
        print("Invalid amount. Must be a number")
        return
    
    try:
        ledger.withdraw(account_number, amount)
        print("Withdrawal successful.")
    except InvalidAmountError as e:
        print(f"Withdrawal failed: {e}")
    except InsufficientFundsError as e:
        print(f"Withdrawal failed: {e}")
        
def show_balance_flow(ledger):
    account_number = input("Account number: ").strip()
    account = ledger.get_account(account_number)
    if account is None:
        print(f"no account found with number {account_number}.")
        return
    account.show_balance()
    
def statement_flow():
    choice = input("View (1=All, 2=Single account): ").strip()
    records = load_transactions(TRANSACTIONS_FILE)
    if choice == "1":
        print_statement(records)
    elif choice == "2":
        account_number = input("Account number: ").strip()
        print_statement(records, account_number=account_number)
    else:
        print("Invalid option.")
        
def sort_flow():
    desc_input = input("descending? (y/n): ").strip().lower()
    desc = desc_input == "y"
    records = load_transactions(TRANSACTIONS_FILE)
    sorted_records = sort_transactions_by_amount(records, desc=desc)
    print_statement(sorted_records)
    
def search_flow():
    try:
        threshold = float(input("Show transaction above amount: ").strip())
    except ValueError:
        print("Invalid amount. Must be a number.")
        return
    records = load_transactions(TRANSACTIONS_FILE)
    results = search_transaction_above(records, threshold)
    print_statement(results)

def clear_screen():
    os.system("cls" if os.name == "nt" else "clear")

def main():
    ledger = Ledger(TRANSACTIONS_FILE)
    
    while True:
        clear_screen()
        print_menu()
        choice = input("Choose an option: ").strip()
        
        if choice == "1":
            clear_screen()
            create_account_flow(ledger)
            input("\nPress Enter to continue...")
        elif choice == "2":
            clear_screen()
            deposit_flow(ledger)
            input("\nPress Enter to continue...")
        elif choice == "3":
            clear_screen()
            withdraw_flow(ledger)
            input("\nPress Enter to continue...")
        elif choice == "4":
            clear_screen()
            show_balance_flow(ledger)
            input("\nPress Enter to continue...")
        elif choice == "5":
            clear_screen()
            ledger.apply_monthly_update_all()
            input("\nPress Enter to continue...")
        elif choice == "6":
            clear_screen()
            statement_flow()
            input("\nPress Enter to continue...")
        elif choice == "7":
            clear_screen()
            sort_flow()
            input("\nPress Enter to continue...")
        elif choice == "8":
            clear_screen()
            search_flow()
            input("\nPress Enter to continue...")
        elif choice == "9":
            clear_screen()
            print("Goodbye!")
            input("\nPress Enter to continue...")
            break
        else:
            clear_screen()
            print("Invalid option, try again.")
            input("\nPress Enter to continue...")
            
if __name__ == "__main__":
    main()