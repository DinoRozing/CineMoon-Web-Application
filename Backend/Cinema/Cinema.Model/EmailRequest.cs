namespace Cinema.Model;
public class EmailRequest
{
    public string UserEmail { get; set; }
    public List<Guid> TicketIds { get; set; }
}
