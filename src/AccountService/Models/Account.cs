namespace AccountService.Models;

public sealed record Account : Entity
{
    public AccountStatus Status { get; set; }
    public string Currency { get; set; }
    public string Name { get; set; }
    
    public override string ToString()  => $" Id: {Id}, Name: {Name}, Currency: {Currency}, Status: {Status}";
}

public enum AccountStatus
{
    Open,
    Settled,
    Closed
}