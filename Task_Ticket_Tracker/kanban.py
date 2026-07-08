def group_by_status(tickets):
    groups = {"To Do": [], "In Progress": [], "Done": []}

    for ticket in tickets:
        status = ticket._status
        if status not in groups:
            groups[status] = []  # handle any unexpected status gracefully
        groups[status].append(ticket)

    return groups


def print_kanban_board(tickets):
    groups = group_by_status(tickets)

    print("--- Kanban Board ---")
    for status in ["To Do", "In Progress", "Done"]:
        print(f"\n{status}:")
        tickets_in_group = groups.get(status, [])
        if len(tickets_in_group) == 0:
            print("  (empty)")
        else:
            for ticket in tickets_in_group:
                print(f"  {ticket.summary()}")
