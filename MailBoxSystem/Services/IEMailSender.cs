namespace MailBoxSystem.Services;

public interface IEMailSender
{
    Task<bool> SensMailAsync(string address, string subject, string body);
}
