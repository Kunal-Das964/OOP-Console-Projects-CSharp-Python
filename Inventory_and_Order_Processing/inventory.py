import os
from models.product import Product
from models.order import Order
from data_structures.stack import Stack
from data_structures.queue import Queue


class Inventory:
    def __init__(self, orders_filepath):
        self._stock = {}
        self._undo_stack = Stack()
        self._shipping_queue = Queue()
        self._order_filepath = orders_filepath
        
    def load_stock(self, filepath):
        if not os.path.exists(filepath):
            print(f"Stock file not found: {filepath}")
            return
        
        with open(filepath, "r") as f:
            for raw_line in f:
                line = raw_line.strip()
                if not line:
                    continue
                
                parts = line.split(",")
                if len(parts) != 3:
                    print(f"Skipped malformed stock line: {line}")
                    continue
                
                name, price_str, quantity_str = parts
                try:
                    price = float(price_str)
                    quantity = int(quantity_str)
                except ValueError:
                    print(f"Skipped malformed stock line: {line}")
                    continue
                
                self._stock[name] = Product(name, price, quantity)
                
    def log_order(self, order):
        os.makedirs(os.path.dirname(self._order_filepath), exist_ok=True)
        with open(self._order_filepath, "a") as f:
            f.write(f"Customer: {order._customer_name} | Time: {order._timestamp} | Total: {order.calculate_total()}\n")
            for product in order._products:
                f.write(f" {product._name}, {product._price}, {product._quantity}\n")
            f.write(("=" * 40) + "\n")
            
    def log_undo_note(self, order):
        os.makedirs(os.path.dirname(self._order_filepath), exist_ok=True)
        with open(self._order_filepath, "a") as f:
            f.write(f"NOTE: Order for {order._customer_name} (originally placed at {order._timestamp}) was UNDONE.\n")
            f.write(("=" * 40) + "\n")

    def place_order(self, cusomter_name, requested_items):
        for product_name, requested_qty in requested_items:
            if product_name not in self._stock:
                print(f"Order rejected: '{product_name}' does bot exist in stock.")
                return None
            available_product = self._stock[product_name]
            if requested_qty > available_product._quantity:
                print(f"Order rejected: not enough stock for '{product_name}'"
                      f"(requested {requested_qty}, available {available_product._quantity}).")
                return None
            
        order = Order(cusomter_name)
        for product_name, requested_qty in requested_items:
            stock_product = self._stock[product_name]
            stock_product._quantity -= requested_qty
            
            ordered_product = Product(product_name, stock_product._price, requested_qty)
            order.add_product(ordered_product)
            
        self._undo_stack.push(order)
        self._shipping_queue.enqueue(order)
        
        print(f"Order placed successfully for {cusomter_name}.")
        return order
    
    def undo_last_order(self):
        order = self._undo_stack.pop()
        if order is None:
            return None
        
        for product in order._products:
            if product._name in self._stock:
                self._stock[product._name]._quantity += product._quantity
        
        self.log_undo_note(order)
        print(f"Order for {order._customer_name}:")
        # order.display_info()
        return order
    
    def ship_next_order(self):
        order = self._shipping_queue.dequeue()
        if order is None:
            return None
        
        print(f"Shipping order for {order._customer_name}")
        order.display_info()
        return order
    
    def get_low_stock_products(self):
        low_stock =[]
        for product in self._stock.values():
            if product._quantity < 5:
                low_stock.append(product)
        return low_stock
    
    def get_all_products(self):
        return list(self._stock.values())