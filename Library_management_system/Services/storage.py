# Ebook,Dune,Herbert,111,4.2,False,1
# Printed,1984,Orwell,222,12,True,3

import os
from models.book import Ebook, PrintedBook
from Services.library import Library  # needed for load_library

def save_library(library, filepath):
    with open(filepath, "w") as f:
        for book in library.get_all_books():
            if isinstance(book, Ebook):
                book_type = "Ebook"
                extra_field = book.filesize
            elif isinstance(book, PrintedBook):
                book_type = "Printed"
                extra_field = book.shelf_number
            else:
                continue
            
            line = f"{book_type}, {book.title}, {book.author}, {book.isbn}, {extra_field}, {book.is_borrowed}, {book.borrow_count}\n"
            f.write(line)
            
def load_library(filepath):
    library = Library()
    
    if not os.path.exists(filepath):
        return library    # for first run- no file yet, return an empty library
    
    with open(filepath, "r") as f:
        for line_number, raw_line in enumerate(f, start=1):
            line = raw_line.strip()
            
            if line == "":
                continue # for skip blank line silently
            
            fields = line.split(",")
            
            if len(fields) != 7:
                print(f"Skipping corrupt line {line_number}: expected 7 fields, found {len(fields)}.")
                continue
            
            book_type, title, author, isbn, extra_field, is_borrowed_str, borrow_count_str = fields
            
            try:
                if book_type == "Ebook":
                    filesize = float(extra_field)
                    book = Ebook(title, author, isbn, filesize)
                elif book_type == "Printed":
                    shelf_number = int(extra_field)
                    book = PrintedBook(title, author, isbn, shelf_number)
                else:
                    print(f"Skipping corrupt line {line_number}: unknown type tag '{book_type}'.")
                    continue
                
                book.is_borrowed = is_borrowed_str.strip().lower() == "true"
                book.borrow_count = int(borrow_count_str.strip())
                
            except ValueError as e:
                print(f"Skipping corrupt line {line_number}: {e}")
                continue
            
            
            library.add_book(book)
            
    return library