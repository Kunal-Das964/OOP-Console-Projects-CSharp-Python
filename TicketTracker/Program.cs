using System;
using System.Collections.Generic;
using System.IO;

public class Program
{
    private static string GetDataPath(string name)
    {
        string dataFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Data");
        return Path.Combine(dataFolder, name);
    }

    private static void PrintMenu()
    {
        Console.WriteLine(GetDataPath("tickets.txt"));
        Console.WriteLine("\n--- Ticket Tracker Menu ---");
        Console.WriteLine("1. Add a ticket");
        Console.WriteLine("2. Update ticket status");
        Console.WriteLine("3. Close a ticket");
        Console.WriteLine("4. View all tickets (sorted by priority)");
        Console.WriteLine("5. View Kanban board");
        Console.WriteLine("6. Exit");
    }

    private static void AddTicketFlow(Tracker tracker)
    {
        Console.Write("Type (1=Base, 2=Bug, 3=Feature): ");
        string type = Console.ReadLine().Trim();

        Console.Write("Ticket ID: ");
        string id = Console.ReadLine().Trim();

        Console.Write("Title: ");
        string title = Console.ReadLine().Trim();

        Console.Write("Priority (High/Medium/Low): ");
        string priority = Console.ReadLine().Trim();

        Ticket ticket;

        if (type == "1")
        {
            ticket = new Ticket(id, title, priority);
        }
        else if (type == "2")
        {
            Console.Write("Severity: ");
            string severity = Console.ReadLine().Trim();
            ticket = new BugTicket(id, title, priority, severity);
        }
        else if (type == "3")
        {
            Console.Write("Effort estimate: ");
            string effort = Console.ReadLine().Trim();
            ticket = new FeatureTicket(id, title, priority, effort);
        }
        else
        {
            Console.WriteLine("Invalid type selected.");
            return;
        }

        tracker.AddTicket(ticket);
    }

    private static void UpdateStatusFlow(Tracker tracker)
    {
        Console.Write("Ticket ID: ");
        string id = Console.ReadLine().Trim();
        Console.Write("New status (To Do / In Progress / Done): ");
        string newStatus = Console.ReadLine().Trim();
        tracker.UpdateStatus(id, newStatus);
    }

    private static void CloseTicketFlow(Tracker tracker)
    {
        Console.Write("Ticket ID: ");
        string id = Console.ReadLine().Trim();
        tracker.CloseTicket(id);
    }

    private static void ViewAllFlow(Tracker tracker)
    {
        List<Ticket> tickets = tracker.GetAllTickets();
        if (tickets.Count == 0)
        {
            Console.WriteLine("No tickets yet.");
            return;
        }
        List<Ticket> sorted = SortHelper.SortTicketsByPriority(tickets);
        Console.WriteLine("--- Tickets (High priority first) ---");
        foreach (Ticket t in sorted)
        {
            Console.WriteLine(t.Summary());
        }
    }

    public static void Main(string[] args)
    {
        string ticketsFile = GetDataPath("tickets.txt");
        Tracker tracker = new Tracker(ticketsFile);
        tracker.LoadFromFile();
        Console.WriteLine($"Loaded {tracker.GetAllTickets().Count} tickets from file.");

        while (true)
        {
            PrintMenu();
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine().Trim();

            switch (choice)
            {
                case "1":
                    AddTicketFlow(tracker);
                    break;
                case "2":
                    UpdateStatusFlow(tracker);
                    break;
                case "3":
                    CloseTicketFlow(tracker);
                    break;
                case "4":
                    ViewAllFlow(tracker);
                    break;
                case "5":
                    KanbanView.PrintKanbanBoard(tracker.GetAllTickets());
                    break;
                case "6":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid option, try again.");
                    break;
            }
        }
    }
}
