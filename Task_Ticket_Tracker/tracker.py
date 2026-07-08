from storage import save_tickets, load_tickets


class Tracker:
    def __init__(self, filepath):
        self._tickets = []
        self._filepath = filepath

    def load_from_file(self):
        self._tickets = load_tickets(self._filepath)

    def add_ticket(self, ticket):
        for existing in self._tickets:
            if existing._id == ticket._id:
                print(f"Ticket with ID {ticket._id} already exists.")
                return False
        self._tickets.append(ticket)
        self._save()
        return True

    def update_status(self, ticket_id, new_status):
        for ticket in self._tickets:
            if ticket._id == ticket_id:
                ticket._status = new_status
                self._save()
                return True
        print(f"No ticket found with ID {ticket_id}.")
        return False

    def close_ticket(self, ticket_id):
        return self.update_status(ticket_id, "Done")

    def get_all_tickets(self):
        return self._tickets

    def _save(self):
        save_tickets(self._tickets, self._filepath)
