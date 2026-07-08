import os
from Services.library import Library
from models.book import Ebook, PrintedBook
from Services.storage import save_library, load_library
from Services.sorting import sorting
from Services.summary import print_summary

BASE_DIR = os.path.dirname(os.path.abspath(__file__))
# LIBRARY_FILE = "data/library.txt"
LIBRARY_FILE = os.path.join(BASE_DIR, "data", "library.txt")

def print_menu():
    print("\n--- Library Menu ---")
    print("1. Add a book")
    print("2. Borrow a book")
    print("3. Return a book")
    print("4. Search by Title")
    print("5. Sort and list books")
    print("6. Show borrowing summary")
    print("7. Save and Exit")
    
def add_book_flow(library):
    book_type = input("Type (1=Ebook, 2=Printed): ").strip()
    title = input("Title: ").strip()
    author = input("Author: ").strip()
    isbn = input("ISBN: ").strip()
    
    if book_type=="1":
        try:
            filesize = float(input("Fileszie (MB): ").strip())
        except ValueError:
            print(f"Invalid filesize. Must be a number.")
            return
        book = Ebook(title, author, isbn, filesize)
    elif book_type == "2":
        try:
            shelf_number = int(input("Shelf number: ").strip())
        except ValueError:
            print("Invalid shelf number. Must be a whole number.")
            return
        book = PrintedBook(title, author, isbn, shelf_number)
    else:
        print("Invalid type selected.")
        return
    
    library.add_book(book)
    
def borrow_flow(library):
    isbn = input("ISBN to borrow: ").strip()
    library.borrow_book(isbn)
    
def return_flow(library):
    isbn = input("ISBN to return: ").strip()
    library.return_book(isbn)
    
def search_flow(library):
    title = input("Title to search: ").strip()
    results = library.search_by_title(title)
    if not results:
        print("No matching books found.")
    for book in results:
        book.display_info()
        
def sort_flow(library):
    sort_by = input("Sort by (title/filesize): ").strip()
    desc_input = input("Descending? (y/n): ").strip().lower()
    desc = desc_input == "y"
    
    all_books = library.get_all_books()
    sorted_books = sorting(all_books, sort_by=sort_by, desc=desc)
    for book in sorted_books:
        book.display_info()

def clear_screen():
    os.system("cls" if os.name == "nt" else "clear")
 
def main():
    # library = Library()
    # load_library(LIBRARY_FILE)
    library = load_library(LIBRARY_FILE)
    
    while True:
        clear_screen()
        print_menu()
        choice = input("Choose an option: ").strip()
        
        if choice == "1":
            clear_screen()
            add_book_flow(library)
            input("\nPress Enter to continue...")
        elif choice == "2":
            clear_screen()
            borrow_flow(library)
            input("\nPress Enter to continue...")
        elif choice == "3":
            clear_screen()
            return_flow(library)
            input("\nPress Enter to continue...")
        elif choice == "4":
            clear_screen()
            search_flow(library)
            input("\nPress Enter to continue...")
        elif choice == "5":
            clear_screen()
            sort_flow(library)
            input("\nPress Enter to continue...")
        elif choice == "6":
            clear_screen()
            print_summary(library)
            input("\nPress Enter to continue...")
        elif choice == "7":
            clear_screen()
            save_library(library, LIBRARY_FILE)
            print("Library saved. GoodBye!")
            input("\nPress Enter to continue...")
            break
        else:
            clear_screen()
            print("Invalid option, try again.")
            input("\nPress Enter to continue...")
    return library
            
if __name__ == "__main__":
    main()