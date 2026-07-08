using System;

public class Ticket
{
    public string Id { get; private set; }
    public string Title { get; private set; }
    public string Priority { get; private set; }
    public string Status { get; private set; }

    public Ticket(string id, string title, string priority, string status = "To Do")
    {
        Id = id;
        Title = title;
        Priority = priority;
        Status = status; // new tickets default to "To Do"
    }

    public void SetStatus(string newStatus)
    {
        Status = newStatus;
    }

    public virtual string Summary()
    {
        return $"[{Id}] {Title} | Priority: {Priority} | Status: {Status}";
    }
}
