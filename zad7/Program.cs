using System;

public abstract class Handler
{
    protected Handler nextHandler;

    public void SetNext(Handler handler)
    {
        nextHandler = handler;
    }

    public abstract void HandleRequest(Ticket ticket);
}

public class TechnicalIssueHandler : Handler
{
    public override void HandleRequest(Ticket ticket)
    {
        if (ticket.Type == TicketType.Technical)
        {
            Console.WriteLine("Handling technical issue.");
        }
        else if (nextHandler != null)
        {
            nextHandler.HandleRequest(ticket);
        }
    }
}

public class BillingQueryHandler : Handler
{
    public override void HandleRequest(Ticket ticket)
    {
        if (ticket.Type == TicketType.Billing)
        {
            Console.WriteLine("Handling billing query.");
        }
        else if (nextHandler != null)
        {
            nextHandler.HandleRequest(ticket);
        }
    }
}

public class GeneralQueryHandler : Handler
{
    public override void HandleRequest(Ticket ticket)
    {
        if (ticket.Type == TicketType.General)
        {
            Console.WriteLine("Handling general query.");
        }
        else if (nextHandler != null)
        {
            nextHandler.HandleRequest(ticket);
        }
    }
}

public class Ticket
{
    public TicketType Type { get; set; }

    public Ticket(TicketType type)
    {
        Type = type;
    }
}

public enum TicketType
{
    Technical,
    Billing,
    General
}

class Program
{
    static void Main(string[] args)
    {
        // Utworzenie handlerów
        Handler technicalHandler = new TechnicalIssueHandler();
        Handler billingHandler = new BillingQueryHandler();
        Handler generalHandler = new GeneralQueryHandler();

      
        technicalHandler.SetNext(billingHandler);
        billingHandler.SetNext(generalHandler);

       
        Ticket[] tickets = {
            new Ticket(TicketType.Technical),
            new Ticket(TicketType.Billing),
            new Ticket(TicketType.General),
            new Ticket(TicketType.Technical)
        };

        foreach (var ticket in tickets)
        {
            technicalHandler.HandleRequest(ticket);
        }
    }
}
