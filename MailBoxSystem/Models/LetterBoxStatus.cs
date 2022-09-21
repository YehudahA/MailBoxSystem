#nullable disable

namespace MailBoxSystem.Models;

public class LetterBoxStatus
{
    public int Id { get; set; }
    public int BoxId { get; set; }

    public DateTime DeliverTime{ get; set; }
    public DateTime? PullTime { get; set; }

    public virtual LetterBox Box { get; set; }
}