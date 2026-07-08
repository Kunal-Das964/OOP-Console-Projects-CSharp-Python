from inventory import Inventory
from budget_finder import find_closest_pair_to_budget
import os

BasePath = os.path.dirname(os.path.abspath(__file__))
STOCK_FILE = os.path.join(BasePath, "data", "stock.txt")
ORDERS_FILE = os.path.join(BasePath, "data", "orders.txt")

def print_menu():
    print("\n--- Inventory Menu ---")
    print("1. Place an order")
    print("2. Undo last order")
    print("3. Ship next pending order")
    print("4. View low-stock alerts")
    print("5. Find two products closest to a budget")
    print("6. Exit")
    
def place_order_flow(inventory):
    customer_name = input("Customer name: ").strip()
    items = []
    
    print("Enter items one at a time. Type 'done' as the product name to finish.")
    while True:
        product_name = input("Product name (or 'done'): ").strip()
        if product_name.lower() == "done":
            break
        
        try:
            quantity = int(input(f"Quantity of '{product_name}': ").strip())
        except ValueError():
            print("Invalid quantity. Must be a whole number. Item not added.")
            continue
        
        items.append((product_name, quantity))
        
    if len(items) == 0:
        print("No items enter. Order cancelled.")
        return
        
    inventory.place_order(customer_name, items)
            
def low_stock_flow(inventory):
    low_stock = inventory.get_low_stock_products()
    if len(low_stock) == 0:
        print("No low-stock items.")
        return
    print("--- Low stock Alerts ---")
    for p in low_stock:
        p.display_info()
        
def budget_flow(inventory):
    try:
        budget = float(input("Enter your budget: ").strip())
    except:
        print("Invalid budget. Must be a number.")
        return
    
    all_prodcuts = inventory.get_all_products()
    result = find_closest_pair_to_budget(all_prodcuts, budget)
    
    if result is not None:
        p1, p2, combined, diff = result
        print(f"Closest pair: {p1._name} + {p2._name} = {combined} (difference form budget: {diff})")
        
        
def main():
    inventory = Inventory(ORDERS_FILE)
    inventory.load_stock(STOCK_FILE)
    
    while True:
        print_menu()
        choice = input("Choose an option: ").strip()
        
        if choice == "1":
            place_order_flow(inventory)
        elif choice == "2":
            inventory.undo_last_order()
        elif choice == "3":
            inventory.ship_next_order()
        elif choice == "4":
            low_stock_flow(inventory)
        elif choice == "5":
            budget_flow(inventory)
        elif choice == "6":
            print("Goodbye!")
            break
        else:
            print("Invalid option, try again")
            
if __name__ == "__main__":
    main()