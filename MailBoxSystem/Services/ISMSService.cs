namespace MailBoxSystem.Services;

public interface ISMSService
{
    Task<bool> SensSMSAsync(int phone, string content);
}
