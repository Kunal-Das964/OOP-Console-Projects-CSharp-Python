# Base class Book (title, author, ISBN) → subclass Ebook (adds filesize) and PrintedBook (adds shelf number). Override a display_info() / DisplayInfo() method in each subclass
class Book:
    def __init__(self, title, author, isbn):
        self.title = title
        self.author = author
        self.isbn = isbn
        self.is_borrowed = False
        self.borrow_count = 0
        
    def display_info(self):
        print(f"\nTitle: {self.title} \nAuthor: {self.author} \nISBN: {self.isbn}")
    
class Ebook(Book):
    def __init__(self, title, author, isbn, filesize):
        super().__init__(title, author, isbn)
        self.filesize = filesize
    
    def display_info(self):
        super().display_info()
        print(f"Type: Ebook \nFilesize: {self.filesize} MB \nBorrow Count: {self.borrow_count}")
        
class PrintedBook(Book):
    def __init__(self, title, author, isbn, shelf_number):
        super().__init__(title, author, isbn)
        self.shelf_number = shelf_number
    
    def display_info(self):
        super().display_info()
        print(f"Type: PrintedBook \nShelf Number: {self.shelf_number} \nBorrow Count: {self.borrow_count}")