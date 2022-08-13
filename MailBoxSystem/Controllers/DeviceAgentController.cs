using Microsoft.AspNetCore.Mvc;

namespace MailBoxSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class DeviceAgentController : ControllerBase
{
    public async Task UpdateBoxesStatus(int deviceId, IReadOnlyList<BoxStatus> boxes)
    {
        // update box status...
        // send mail/sms if status changed...

        throw new NotImplementedException();
    }

    public sealed record BoxStatus(string BoxId, bool IsFUll);
}
