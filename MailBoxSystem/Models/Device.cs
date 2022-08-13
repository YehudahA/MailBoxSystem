#nullable disable

namespace MailBoxSystem.Models;

public class Device
{
    public int Id { get; set; }
    public string Address { get; set; }
    public string UserId { get; set; }
    
    public virtual User Manager { get; set; }
    public virtual ICollection<Box> Boxes { get; set; } = new HashSet<Box>();
}
