from Services.sorting import sorting

def print_summary(library):
    total = library.get_total_books()
    borrowed = library.get_borrow_count()
    all_books = library.get_all_books()
    
    top_3 = sorting(all_books, sort_by="borrow_count", desc=True)[:3]
    
    print("--- Library Summary ---")
    print(f"Total Books: {total}")
    print(f"Currently Borrowed: {borrowed}")
    print("Top 3 most borrowed:")
    if len(top_3) == 0:
        print("No books in library.")
    else:
        for i, book in enumerate(top_3, start=1):
            print(f"{i}. {book.title} (borrowed) {book.borrow_count} times")