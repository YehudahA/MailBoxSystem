using System.Xml.Linq;

namespace MailBoxSystem.Services;

public interface ISmsService
{
    Task<string> SendAsync(string phone, string message);
}

public sealed class SmsService : ISmsService
{
    #region ctor, fields

    private readonly HttpClient httpClient;
    private readonly IConfiguration configuration;

    public SmsService(HttpClient httpClient, IConfiguration configuration)
    {
        this.httpClient = httpClient;
        this.configuration = configuration;
    }

    #endregion

    public async Task<string> SendAsync(string phone, string message)
    {
        var settings = configuration.GetSection("SMS");

        var userName = settings["userName"];
        var password = settings["password"];
        var sender = settings["sender"];

        var messageText = System.Security.SecurityElement.Escape(message);

        var xdoc = new XElement("Inforu",
            new XElement("User",
                new XElement("Username", userName),
                new XElement("Password", password)),
            new XElement("Content",
                new XAttribute("Type", "sms"),
                new XElement("Message", messageText)),
            new XElement("Recipients",
                new XElement("PhoneNumber", phone)),
            new XElement("Settings",
                new XElement("Sender", sender),
                new XElement("MessageInterval", 0),
                new XElement("TimeToSend", ""))
            );

        var xml = xdoc.ToString(SaveOptions.DisableFormatting);
        var data = new Dictionary<string, string>()
        {
            ["InforuXML"] = xml
        };
        string result = await PostDataToURLAsync(data);
        return result;
    }

    private async Task<string> PostDataToURLAsync(IEnumerable<KeyValuePair<string, string>> data)
    {
        var url = "http://api.inforu.co.il/SendMessageXml.ashx";

        var content = new FormUrlEncodedContent(data);
        var response = await httpClient.PostAsync(url, content);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadAsStringAsync();
        return result;
    }
}
