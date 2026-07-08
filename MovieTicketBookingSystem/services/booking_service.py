from models.booking import Booking


class BookingService:

    def __init__(self, show):
        self.show = show
        self.bookings = []

    def get_seat_position(self, seat):
        seat = seat.upper()

        row = ord(seat[0]) - ord('A')
        col = int(seat[1:]) - 1

        return row, col

    def is_valid_seat(self, row, col):

        if row < 0 or row >= self.show.rows:
            return False

        if col < 0 or col >= self.show.cols:
            return False

        return True

    def is_available(self, row, col):

        return self.show.seats[row][col] == "O"

    def book_seats(self, customer_name, seat_numbers):

        booked = []

        # Validate all seats first
        for seat in seat_numbers:

            row, col = self.get_seat_position(seat)

            if not self.is_valid_seat(row, col):
                print(f"\n{seat} is not a valid seat.")
                return

            if not self.is_available(row, col):
                print(f"\n{seat} is already booked.")
                return

        # Book all seats
        for seat in seat_numbers:

            row, col = self.get_seat_position(seat)

            self.show.seats[row][col] = "X"

            booked.append(seat.upper())

        booking = Booking(
            customer_name,
            self.show.movie_name,
            self.show.show_time,
            booked
        )

        self.bookings.append(booking)

        print("\nBooking Successful!")

        print("\nBooking Details")
        print("----------------------")
        print("Customer :", booking.customer_name)
        print("Movie    :", booking.movie_name)
        print("Time     :", booking.show_time)
        print("Seats    :", ", ".join(booking.seat_numbers))