from models.show import Show
from services.booking_service import BookingService


def main():

    show = Show("Avengers: Endgame", "6:00 PM", 5, 5)

    booking_service = BookingService(show)

    while True:

        print("\n========== Movie Ticket Booking ==========")
        print("1. Display Seats")
        print("2. Book Seats")
        print("3. Exit")

        choice = input("Enter your choice: ")

        if choice == "1":

            show.display_seats()

        elif choice == "2":

            customer_name = input("Enter Customer Name: ")

            seats = input("Enter Seat Numbers (Example: A1,A2,B3): ")

            seat_list = []

            for seat in seats.split(","):
                seat_list.append(seat.strip())

            booking_service.book_seats(customer_name, seat_list)

        elif choice == "3":

            print("\nThank you for using the Movie Ticket Booking System.")
            break

        else:

            print("\nInvalid Choice!")


if __name__ == "__main__":
    main()