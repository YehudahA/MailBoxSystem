#nullable disable

namespace MailBoxSystem.Models;

public abstract class Box
{
    public int Id { get; set; }
    public int DeviceId { get; set; }
    public string LocalId { get; set; }

    /// <summary>
    /// Current status of the box.
    /// </summary>
    public bool IsFull { get; set; }
    
    public virtual Device Device { get; set; }
}

public class LetterBox : Box
{
    public string Line1 { get; set; }
    public string Line2 { get; set; }
    public string OwnerId { get; set; }

    public virtual User Owner { get; set; }
}

public class PackageBox : Box
{
    public PackBoxSize Size { get; set; }
}

public enum PackBoxSize
{
    Small = 1,
    Medium,
    Large
}