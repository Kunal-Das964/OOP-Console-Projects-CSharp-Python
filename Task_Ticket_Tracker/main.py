from tracker import Tracker
from models.ticket import Ticket, BugTicket, FeatureTicket
from sorting import sort_tickets_by_priority
from kanban import print_kanban_board
import os


BASE_DIR = os.path.dirname(os.path.abspath(__file__))
TICKETS_FILE = os.path.join(BASE_DIR, "data", "tickets.txt")


def print_menu():
    print("\n--- Ticket Tracker Menu ---")
    print("1. Add a ticket")
    print("2. Update ticket status")
    print("3. Close a ticket")
    print("4. View all tickets (sorted by priority)")
    print("5. View Kanban board")
    print("6. Exit")


def add_ticket_flow(tracker):
    ticket_type = input("Type (1=Base, 2=Bug, 3=Feature): ").strip()
    ticket_id = input("Ticket ID: ").strip()
    title = input("Title: ").strip()
    priority = input("Priority (High/Medium/Low): ").strip()

    if ticket_type == "1":
        ticket = Ticket(ticket_id, title, priority)
    elif ticket_type == "2":
        severity = input("Severity: ").strip()
        ticket = BugTicket(ticket_id, title, priority, severity)
    elif ticket_type == "3":
        effort = input("Effort estimate: ").strip()
        ticket = FeatureTicket(ticket_id, title, priority, effort)
    else:
        print("Invalid type selected.")
        return

    tracker.add_ticket(ticket)


def update_status_flow(tracker):
    ticket_id = input("Ticket ID: ").strip()
    new_status = input("New status (To Do / In Progress / Done): ").strip()
    tracker.update_status(ticket_id, new_status)


def close_ticket_flow(tracker):
    ticket_id = input("Ticket ID: ").strip()
    tracker.close_ticket(ticket_id)


def view_all_flow(tracker):
    tickets = tracker.get_all_tickets()
    if len(tickets) == 0:
        print("No tickets yet.")
        return
    sorted_tickets = sort_tickets_by_priority(tickets)
    print("--- Tickets (High priority first) ---")
    for t in sorted_tickets:
        print(t.summary())


def main():
    tracker = Tracker(TICKETS_FILE)
    tracker.load_from_file()
    print(f"Loaded {len(tracker.get_all_tickets())} tickets from file.")

    while True:
        print_menu()
        choice = input("Choose an option: ").strip()

        if choice == "1":
            add_ticket_flow(tracker)
        elif choice == "2":
            update_status_flow(tracker)
        elif choice == "3":
            close_ticket_flow(tracker)
        elif choice == "4":
            view_all_flow(tracker)
        elif choice == "5":
            print_kanban_board(tracker.get_all_tickets())
        elif choice == "6":
            print("Goodbye!")
            break
        else:
            print("Invalid option, try again.")


if __name__ == "__main__":
    main()
