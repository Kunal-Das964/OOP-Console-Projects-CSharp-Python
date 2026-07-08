using System;
using System.Collections.Generic;

public static class KanbanView
{
    public static Dictionary<string, List<Ticket>> GroupByStatus(List<Ticket> tickets)
    {
        Dictionary<string, List<Ticket>> groups = new Dictionary<string, List<Ticket>>
        {
            { "To Do", new List<Ticket>() },
            { "In Progress", new List<Ticket>() },
            { "Done", new List<Ticket>() }
        };

        foreach (Ticket ticket in tickets)
        {
            string status = ticket.Status;
            if (!groups.ContainsKey(status))
            {
                groups[status] = new List<Ticket>(); // handle any unexpected status gracefully
            }
            groups[status].Add(ticket);
        }

        return groups;
    }

    public static void PrintKanbanBoard(List<Ticket> tickets)
    {
        Dictionary<string, List<Ticket>> groups = GroupByStatus(tickets);

        Console.WriteLine("--- Kanban Board ---");
        foreach (string status in new[] { "To Do", "In Progress", "Done" })
        {
            Console.WriteLine($"\n{status}:");
            List<Ticket> ticketsInGroup = groups.ContainsKey(status) ? groups[status] : new List<Ticket>();

            if (ticketsInGroup.Count == 0)
            {
                Console.WriteLine("  (empty)");
            }
            else
            {
                foreach (Ticket ticket in ticketsInGroup)
                {
                    Console.WriteLine($"  {ticket.Summary()}");
                }
            }
        }
    }
}
