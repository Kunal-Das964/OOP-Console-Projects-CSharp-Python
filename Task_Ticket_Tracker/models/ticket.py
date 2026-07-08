class Ticket:
    def __init__(self, ticket_id, title, priority, status="To Do"):
        self._id = ticket_id
        self._title = title
        self._priority = priority
        self._status = status  # new tickets default to "To Do"

    def summary(self):
        return f"[{self._id}] {self._title} | Priority: {self._priority} | Status: {self._status}"


class BugTicket(Ticket):
    def __init__(self, ticket_id, title, priority, severity, status="To Do"):
        super().__init__(ticket_id, title, priority, status)
        self._severity = severity

    def summary(self):
        base_summary = super().summary()
        return f"{base_summary} | Severity: {self._severity}"


class FeatureTicket(Ticket):
    def __init__(self, ticket_id, title, priority, effort_estimate, status="To Do"):
        super().__init__(ticket_id, title, priority, status)
        self._effort_estimate = effort_estimate

    def summary(self):
        base_summary = super().summary()
        return f"{base_summary} | Effort Estimate: {self._effort_estimate}"
