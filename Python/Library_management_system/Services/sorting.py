from models.book import Ebook

def sorting(books, sort_by, desc=False):
    sorted_books = books.copy()
    n = len(sorted_books)
    
    for i in range(n-1):
        swap = False
        for j in range(n-1-i):
            first = sorted_books[j]
            second = sorted_books[j+1]
            
            key1 = get_sort_key(first, sort_by)
            key2 = get_sort_key(second, sort_by)
            
            if desc:
                swapped = key1 < key2
            else:
                swapped = key1 > key2
                
            if swapped:
                sorted_books[j], sorted_books[j+1] = sorted_books[j+1], sorted_books[j]
                swap = True
                
        if not swap:
            break
    return sorted_books
            
def get_sort_key(book, sort_by):
    if sort_by == "title":
        return book.title.lower()
    elif sort_by == "filesize":
        if isinstance(book, Ebook):
            return book.filesize
        else:
            return 0
    elif sort_by == "borrow_count":
        return book.borrow_count
    else:
        raise ValueError(f"Unknown field for sorting: {sort_by}.")