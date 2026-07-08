import os
from models.ticket import Ticket, BugTicket, FeatureTicket

# Line format: type,id,title,priority,status,extra
# type = "Base", "Bug", or "Feature"
# extra = "" for Base, severity for Bug, effort_estimate for Feature


def save_tickets(tickets, filepath):
    os.makedirs(os.path.dirname(filepath), exist_ok=True)
    with open(filepath, "w") as f:
        for ticket in tickets:
            if isinstance(ticket, BugTicket):
                ticket_type = "Bug"
                extra = ticket._severity
            elif isinstance(ticket, FeatureTicket):
                ticket_type = "Feature"
                extra = ticket._effort_estimate
            else:
                ticket_type = "Base"
                extra = ""

            line = f"{ticket_type},{ticket._id},{ticket._title},{ticket._priority},{ticket._status},{extra}\n"
            f.write(line)


def load_tickets(filepath):
    tickets = []

    if not os.path.exists(filepath):
        return tickets  # no file yet means no tickets yet

    with open(filepath, "r") as f:
        for raw_line in f:
            line = raw_line.strip()
            if not line:
                continue

            parts = line.split(",")
            if len(parts) != 6:
                print(f"Skipped malformed ticket line: {line}")
                continue

            ticket_type, ticket_id, title, priority, status, extra = parts

            if ticket_type == "Bug":
                ticket = BugTicket(ticket_id, title, priority, extra, status)
            elif ticket_type == "Feature":
                ticket = FeatureTicket(ticket_id, title, priority, extra, status)
            elif ticket_type == "Base":
                ticket = Ticket(ticket_id, title, priority, status)
            else:
                print(f"Skipped ticket line with unknown type '{ticket_type}': {line}")
                continue

            tickets.append(ticket)

    return tickets
