#nullable disable

namespace MailBoxSystem.Models;

public abstract class Box
{
    public int Id { get; set; }
    public int LocalNumber { get; set; }
}

public class LetterBox : Box
{
    public string Line1 { get; set; }
    public string Line2 { get; set; }
    public int OwnerId { get; set; }

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