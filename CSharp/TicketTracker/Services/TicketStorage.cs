using System;
using System.Collections.Generic;
using System.IO;

public static class TicketStorage
{
    // Line format: type,id,title,priority,status,extra
    // type = "Base", "Bug", or "Feature"
    // extra = "" for Base, severity for Bug, effort estimate for Feature

    public static void SaveTickets(List<Ticket> tickets, string filepath)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(filepath));
        using (StreamWriter writer = new StreamWriter(filepath, false)) // false = overwrite
        {
            foreach (Ticket ticket in tickets)
            {
                string ticketType;
                string extra;

                if (ticket is BugTicket bug)
                {
                    ticketType = "Bug";
                    extra = bug.Severity;
                }
                else if (ticket is FeatureTicket feature)
                {
                    ticketType = "Feature";
                    extra = feature.EffortEstimate;
                }
                else
                {
                    ticketType = "Base";
                    extra = "";
                }

                writer.WriteLine($"{ticketType},{ticket.Id},{ticket.Title},{ticket.Priority},{ticket.Status},{extra}");
            }
        }
    }

    public static List<Ticket> LoadTickets(string filepath)
    {
        List<Ticket> tickets = new List<Ticket>();

        if (!File.Exists(filepath))
        {
            return tickets; // no file yet means no tickets yet
        }

        foreach (string rawLine in File.ReadAllLines(filepath))
        {
            string line = rawLine.Trim();
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            string[] parts = line.Split(',');
            if (parts.Length != 6)
            {
                Console.WriteLine($"Skipped malformed ticket line: {line}");
                continue;
            }

            string ticketType = parts[0];
            string id = parts[1];
            string title = parts[2];
            string priority = parts[3];
            string status = parts[4];
            string extra = parts[5];

            Ticket ticket;

            if (ticketType == "Bug")
            {
                ticket = new BugTicket(id, title, priority, extra, status);
            }
            else if (ticketType == "Feature")
            {
                ticket = new FeatureTicket(id, title, priority, extra, status);
            }
            else if (ticketType == "Base")
            {
                ticket = new Ticket(id, title, priority, status);
            }
            else
            {
                Console.WriteLine($"Skipped ticket line with unknown type '{ticketType}': {line}");
                continue;
            }

            tickets.Add(ticket);
        }

        return tickets;
    }
}
