import os

# Line format: account_number,transaction_type,amount,timestamp

def append_transaction(transaction, filepath):
    os.makedirs(os.path.dirname(filepath), exist_ok=True)
    with open(filepath, "a") as f:
        line = f"{transaction.account_number}, {transaction.transaction_type}, {transaction.amount}, {transaction.timestamp}\n"
        f.write(line)
        
def load_transactions(filepath):
    records = []
    if not os.path.exists(filepath):
        return records
    
    with open(filepath, "r") as f:
        for line in f:
            line = line.strip()
            if not line:
                continue
            parts = line.split(",")
            if len(parts) != 4:
                continue
            account_number, transaction_type, amount, timestamp = parts
            records.append({
                "account_number": account_number,
                "transaction_type": transaction_type,
                "amount": amount,
                "timestamp": timestamp
            })
    return records

def print_statement(records, account_number=None):
    print("--- Statement ---")
    for r in records:
        if account_number is not None and r["account_number"] != account_number:
            continue
        print(f"[{r['timestamp']}] {r['account_number']} | {r['transaction_type']} | {r['amount']}")