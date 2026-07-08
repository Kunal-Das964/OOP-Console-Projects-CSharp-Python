from sorting import sort_products_by_price

def find_closest_pair_to_budget(products, budget):
    if len(products) < 2:
        print("Not enough products to form a pair.")
        return None
    
    sorted_products = sort_products_by_price(products)
    
    left = 0
    right = len(sorted_products) - 1
    
    best_pair = None
    best_difference = float("inf")
    
    while left < right:
        product_left = sorted_products[left]
        product_right = sorted_products[right]
        combined_price = product_left._price + product_right._price
        difference = abs(combined_price - budget)
        
        if difference < best_difference:
            best_difference = difference
            best_pair = (product_left, product_right, combined_price, difference)
            
        if combined_price < budget:
            left+=1
        elif combined_price > budget:
            right -= 1
        else:
            break
        
    return best_pair