#nullable disable

namespace MailBoxSystem.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string EMail { get; set; }
    public int PhoneNumber { get; set; }
    public string Password { get; set; }
    public string TempToken { get; set; }
}
