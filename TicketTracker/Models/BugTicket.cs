public class BugTicket : Ticket
{
    public string Severity { get; private set; }

    public BugTicket(string id, string title, string priority, string severity, string status = "To Do")
        : base(id, title, priority, status)
    {
        Severity = severity;
    }

    public override string Summary()
    {
        string baseSummary = base.Summary();
        return $"{baseSummary} | Severity: {Severity}";
    }
}
