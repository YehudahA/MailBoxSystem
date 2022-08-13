#nullable disable

namespace MailBoxSystem.Models;

public class User
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string EMail { get; set; }
    public string PhoneNumber { get; set; }
    public string PasswordHash { get; set; }
}
