def sort_transactions_by_amount(records, desc=False):
    sorted_records = records.copy()
    n = len(sorted_records)
    
    for i in range(n):
        for j in range(0, n-i-1):
            amount_j = float(sorted_records[j]["amount"])
            amount_j1 = float(sorted_records[j+1]["amount"])
            
            if desc:
                swap = amount_j < amount_j1
            else:
                swap = amount_j > amount_j1
                
            if swap:
                sorted_records[j], sorted_records[j+1] = sorted_records[j+1], sorted_records[j]
    return sorted_records
    
def search_transaction_above(records, threshold):
    results = []
    for r in records:
        if float(r["amount"]) > threshold:
            results.append(r)
    return results
        