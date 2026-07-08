class Product():
    def __init__(self, name, price, quantity):
        self._name = name
        self._price = price
        self._quantity = quantity
        
    def display_info(self):
        print(f"Product: {self._name} | Price: {self._price} | Quantity: {self._quantity}")