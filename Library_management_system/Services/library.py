#Library class internally manages a dictionary/Dictionary of books with add_book(), borrow_book(), return_book(), search_by_title()
from models.book import Book

class Library:
    def __init__(self):
        self.books = {} # Key : isbn, value: Book object
    
    # Add book
    def add_book(self, book):
        if book.isbn in self.books:
            print(f"Book with ISBN {book.isbn} already exists.")
            return False
        self.books[book.isbn] = book
        # print(f"\nBook {book.title} added to the library.")
        return True
    
    # Borrow book
    def borrow_book(self, isbn):
        if isbn not in self.books:
            print(f"No book found with ISBN {isbn}.")
            return False
        book = self.books[isbn]
        if book.is_borrowed:
            print(f"Book '{book.title}' is already borrowed.")
            return False
        book.is_borrowed = True
        book.borrow_count += 1
        print(f"\nBook '{book.title}' is borrowed.")
        return True
    
    # Return book
    def return_book(self, isbn):
        if isbn not in self.books:
            print(f"No book found with ISBN {isbn}.")
            return False
        book = self.books[isbn]
        if not book.is_borrowed:
            print(f"Book '{book.title}' was not borrowed.")
            return False
        book.is_borrowed = False
        print(f"Book '{book.title}' is returned.")
        return True
    
    # search book by title
    def search_by_title(self, title):
        results = []
        for book in self.books.values():
            if title.lower() in book.title.lower():
                results.append(book)
        return results
    
    # Getting all books
    def get_all_books(self):
        return list(self.books.values())
    
    # getting total books
    def get_total_books(self):
        return len(self.books)
    
    # getting currently borrowed count
    def get_borrow_count(self):
        count = 0
        for book in self.books.values():
            if book.is_borrowed:
                count += 1
        return count