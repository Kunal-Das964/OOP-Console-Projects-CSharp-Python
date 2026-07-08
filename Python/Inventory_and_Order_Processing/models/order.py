from datetime import datetime

class Order:
    def __init__(self, customer_name):
        self._customer_name = customer_name
        self._products = []
        self._timestamp = datetime.now()
        
    def add_product(self, product):
        self._products.append(product)
        
    def calculate_total(self):
        total = 0
        for product in self._products:
            total += product._price * product._quantity
        return total
    
    def display_info(self):
        print(f"Order for: {self._customer_name} | Time: {self._timestamp}")
        for product in self._products:
            product.display_info()
        print(f"Order Total: {self.calculate_total()}")