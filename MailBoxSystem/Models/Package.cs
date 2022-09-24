#nullable disable

namespace MailBoxSystem.Models;

public class Package
{
    public int Id { get; set; }
    public string Code { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public int SenderId { get; set; }

    public string RecieverName { get; set; }
    public int RecieverPhone { get; set; }


    /// <summary>
    /// Which Box the sender put the package
    /// </summary>
    public int? BoxId { get; set; }
    public DateTime? DeliverTime { get; set; }

    public DateTime? PullTime { get; set; }

    public virtual PackageBox Box { get; set; }
    public virtual PackageSender Sender { get; set; }
}
