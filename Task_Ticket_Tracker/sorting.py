def priority_rank(priority):
    ranks = {"High": 3, "Medium": 2, "Low": 1}
    return ranks.get(priority, 0)  # unknown priority sinks to the bottom


def sort_tickets_by_priority(tickets):
    sorted_tickets = tickets.copy()  # don't mutate the original list
    n = len(sorted_tickets)

    for i in range(n):
        for j in range(0, n - i - 1):
            rank_j = priority_rank(sorted_tickets[j]._priority)
            rank_j1 = priority_rank(sorted_tickets[j + 1]._priority)

            if rank_j < rank_j1:  # descending: High > Medium > Low
                sorted_tickets[j], sorted_tickets[j + 1] = sorted_tickets[j + 1], sorted_tickets[j]

    return sorted_tickets
