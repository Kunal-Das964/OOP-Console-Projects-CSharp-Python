public class FeatureTicket : Ticket
{
    public string EffortEstimate { get; private set; }

    public FeatureTicket(string id, string title, string priority, string effortEstimate, string status = "To Do")
        : base(id, title, priority, status)
    {
        EffortEstimate = effortEstimate;
    }

    public override string Summary()
    {
        string baseSummary = base.Summary();
        return $"{baseSummary} | Effort Estimate: {EffortEstimate}";
    }
}
