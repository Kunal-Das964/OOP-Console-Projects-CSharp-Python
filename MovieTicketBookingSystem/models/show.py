class Show:
    def __init__(self, movie_name, show_time, rows, cols):
        self.movie_name = movie_name
        self.show_time = show_time
        self.rows = rows
        self.cols = cols

        # Create 2D seating layout
        self.seats = []

        for i in range(rows):
            row = []

            for j in range(cols):
                row.append("O")

            self.seats.append(row)
            
    def display_seats(self):

        print(f"\nMovie : {self.movie_name}")
        print(f"Time  : {self.show_time}")

        # Print column numbers
        print("  ", end=" ")

        for col in range(self.cols):
            print(col + 1, end=" ")

        print()

        # Print each row
        for row in range(self.rows):

            print(chr(65 + row), end="  ")

            for col in range(self.cols):
                print(self.seats[row][col], end=" ")

            print()