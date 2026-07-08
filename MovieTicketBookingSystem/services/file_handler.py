import os
from models.booking import Booking


class FileHandler:

    FILE_PATH = "data/bookings.txt"

    @staticmethod
    def save_booking(booking):

        with open(FileHandler.FILE_PATH, "a") as file:

            line = (
                booking.customer_name + "," +
                booking.movie_name + "," +
                booking.show_time + "," +
                "|".join(booking.seat_numbers)
            )

            file.write(line + "\n")

    @staticmethod
    def load_bookings(show):

        bookings = []

        if not os.path.exists(FileHandler.FILE_PATH):
            return bookings

        with open(FileHandler.FILE_PATH, "r") as file:

            for line in file:

                line = line.strip()

                if line == "":
                    continue

                try:

                    parts = line.split(",")

                    customer = parts[0]
                    movie = parts[1]
                    time = parts[2]
                    seats = parts[3].split("|")

                    booking = Booking(
                        customer,
                        movie,
                        time,
                        seats
                    )

                    bookings.append(booking)

                    for seat in seats:

                        row = ord(seat[0]) - ord("A")
                        col = int(seat[1:]) - 1

                        show.seats[row][col] = "X"

                except Exception:

                    print("Invalid line skipped:", line)

        return bookings