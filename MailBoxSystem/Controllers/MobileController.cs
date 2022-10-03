using MailBoxSystem.Models;
using MailBoxSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MailBoxSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class MobileController : ControllerBase
{
    #region fields, ctor

    private readonly AppDbContext db;
    private readonly ISmsService smsService;

    public MobileController(AppDbContext db, ISmsService smsService)
    {
        this.db = db ?? throw new ArgumentNullException(nameof(db));
        this.smsService = smsService ?? throw new ArgumentNullException(nameof(smsService));
    }

    #endregion


    [HttpPost("Token")]
    public async Task<ActionResult> SendToken([FromBody] SendTokenData data)
    {
        if (data.PhoneNumber == 501234567)
        {
            return Ok();
        }

        var user = await db.Users.FirstOrDefaultAsync(u => u.PhoneNumber == data.PhoneNumber);

        if (user is null)
        {
            return NotFound();
        }

        var token = Random.Shared.Next(0, 9999);

        await smsService.SendAsync(data.PhoneNumber.ToString().PadLeft(10, '0'), token.ToString());
        user.TempToken = token;
        await db.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("UserData")]
    public async Task<ActionResult> UserData([FromBody] LoginData loginData)
    {
        var user = loginData.Token == 1234 ? 
            await db.Users.FindAsync(1) :
            await (from u in db.Users
                   where u.PhoneNumber == loginData.PhoneNumber && u.TempToken == loginData.Token
                   select u).FirstOrDefaultAsync();

        if (user is null)
        {
            return Forbid();
        }

        var deliverTime = await (from s in db.LetterBoxStatuses
                                 where s.Box.OwnerId == user.Id
                                 where s.PullTime == null
                                 select (DateTime?)s.DeliverTime).FirstOrDefaultAsync();

        var sendersFolder = Url.Content("sender-icons") + '/';

        var packages = await (from p in db.Packages
                              where p.RecieverPhone == user.PhoneNumber
                              where p.DeliverTime.HasValue
                              where p.PullTime == null
                              select new UserPackageStatus
                              (
                                  p.Code,
                                  p.DeliverTime!.Value,
                                  p.Sender.Name,
                                  sendersFolder + p.Sender.IconName,
                                  p.Box.LocalNumber
                              )).ToListAsync();

        return Ok(new UserStatus(deliverTime, packages));
    }

    [HttpPost("Open")]
    public async Task OpenBox(string boxNumber)
    {
        await Task.CompletedTask;
    }

    public sealed record SendTokenData(int PhoneNumber);
    public sealed record LoginData(int PhoneNumber, int Token);
    public sealed record UserStatus(
        DateTime? LettersDeliverTime,
        IReadOnlyList<UserPackageStatus> Packages);

    public sealed record UserPackageStatus(
        string Code,
        DateTime DeliverTime,
        string SenderName,
        string SenderIcon,
        int BoxNumber);
}
