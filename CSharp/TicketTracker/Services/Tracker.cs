using System;
using System.Collections.Generic;

public class Tracker
{
    private List<Ticket> _tickets = new List<Ticket>();
    private string _filepath;

    public Tracker(string filepath)
    {
        _filepath = filepath;
    }

    public void LoadFromFile()
    {
        _tickets = TicketStorage.LoadTickets(_filepath);
    }

    public bool AddTicket(Ticket ticket)
    {
        foreach (Ticket existing in _tickets)
        {
            if (existing.Id == ticket.Id)
            {
                Console.WriteLine($"Ticket with ID {ticket.Id} already exists.");
                return false;
            }
        }
        _tickets.Add(ticket);
        Save();
        return true;
    }

    public bool UpdateStatus(string ticketId, string newStatus)
    {
        foreach (Ticket ticket in _tickets)
        {
            if (ticket.Id == ticketId)
            {
                ticket.SetStatus(newStatus);
                Save();
                return true;
            }
        }
        Console.WriteLine($"No ticket found with ID {ticketId}.");
        return false;
    }

    public bool CloseTicket(string ticketId)
    {
        return UpdateStatus(ticketId, "Done");
    }

    public List<Ticket> GetAllTickets()
    {
        return _tickets;
    }

    private void Save()
    {
        TicketStorage.SaveTickets(_tickets, _filepath);
    }
}
