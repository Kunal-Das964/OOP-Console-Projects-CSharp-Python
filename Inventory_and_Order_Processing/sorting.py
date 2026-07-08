from models.product import Product


def quicksort(products):
    if len(products) <= 1:
        return products
    
    pivot = products[-1]
    pivot_price = pivot._price
    
    left = []
    middle = []
    right = []
    
    for p in products:
        if p._price < pivot_price:
            left.append(p)
        elif p._price > pivot_price:
            right.append(p)
        else:
            middle.append(p)
    
    
    return quicksort(left) + middle + quicksort(right)

def sort_products_by_price(products):
    return quicksort(products)
    