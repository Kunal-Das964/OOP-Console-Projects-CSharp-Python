using System.Collections.Generic;

public static class SortHelper
{
    public static int PriorityRank(string priority)
    {
        switch (priority)
        {
            case "High":
                return 3;
            case "Medium":
                return 2;
            case "Low":
                return 1;
            default:
                return 0; // unknown priority sinks to the bottom
        }
    }

    public static List<Ticket> SortTicketsByPriority(List<Ticket> tickets)
    {
        List<Ticket> sortedTickets = new List<Ticket>(tickets); // copy, don't mutate original
        int n = sortedTickets.Count;

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                int rankJ = PriorityRank(sortedTickets[j].Priority);
                int rankJ1 = PriorityRank(sortedTickets[j + 1].Priority);

                if (rankJ < rankJ1) // descending: High > Medium > Low
                {
                    Ticket temp = sortedTickets[j];
                    sortedTickets[j] = sortedTickets[j + 1];
                    sortedTickets[j + 1] = temp;
                }
            }
        }

        return sortedTickets;
    }
}
