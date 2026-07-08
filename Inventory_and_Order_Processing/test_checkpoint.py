from models.product import Product
from models.order import Order
from data_structures.queue import Queue
from data_structures.stack import Stack
from inventory import Inventory
import os


BasePath = os.path.dirname(os.path.abspath(__file__))
stock_filepath = os.path.join(BasePath, "data", "stock.txt")
order_filepath = os.path.join(BasePath, "data", "orders.txt")


# p1 = Product("Laptop", 800, 1)
# p2 = Product("Mouse", 25, 2)
# order = Order("Kunal")
# order.add_product(p1)
# order.add_product(p2)
# order.display_info()

# s = Stack()
# s.push(1)
# s.push(2)
# s.push(3)
# print(s.pop())   # expect 3 (last in, first out)
# print(s.pop())   # expect 2
# print(s.peek())  # expect 1, not removed
# print(s.is_empty())  # expect False
# q = Queue()
# q.enqueue("a")
# q.enqueue("b")
# q.enqueue("c")
# print(q.dequeue())  # expect "a" (first in, first out)
# print(q.dequeue())  # expect "b"
# print(q.peek())      # expect "c", not removed
# empty_stack = Stack()
# empty_stack.pop()  # should print graceful message, not crash



inv = Inventory(order_filepath)
inv.load_stock(stock_filepath)


order1 = inv.place_order("Kunal", [("Laptop", 1), ("Mouse", 2)])
inv.place_order("Priya", [("Keyboard", 100)])  # should be rejected, not enough stock
# for p in inv.get_low_stock_products():
#     p.display_info()

inv.undo_last_order()
# inv.ship_next_order()