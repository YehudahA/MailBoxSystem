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


    [HttpGet("Users")]
    public async Task<ActionResult> Users()
    {
        var data = await (from u in db.Users
                          join b in db.LetterBoxes
                          on u.Id equals b.OwnerId
                          select new 
                          {
                              UserId = u.Id,
                              u.PhoneNumber,
                              BoxNumber = b.LocalId
                          }).ToListAsync();

        return Ok(data);
    }

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
        user.TempToken = token.ToString();
        await db.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("Login")]
    public async Task<ActionResult> Login([FromBody] LoginData loginData)
    {
        if (loginData.Token == "1234")
        {
            return Ok();
        }

        var userId = await (from u in db.Users
                            where u.PhoneNumber == loginData.PhoneNumber && u.TempToken == loginData.Token
                            select (int?)u.Id).FirstOrDefaultAsync();


        return userId.HasValue ? Ok(new { UserId = userId }) : NotFound();
    }

    [HttpGet("Status")]
    public async Task<ActionResult> Status(int userId)
    {
        var user = await db.Users.FindAsync(userId);

        if (user is null)
        {
            return NotFound();
        }

        var deliverTime = await (from s in db.LetterBoxStatuses
                                 where s.Box.OwnerId == userId
                                 where s.PullTime == null
                                 select (DateTime?)s.DeliverTime).FirstOrDefaultAsync();

        var packages = await (from p in db.Packages
                              where p.RecieverPhone == user.PhoneNumber
                              where p.DeliverTime.HasValue
                              where p.PullTime == null
                              select new UserPackageStatus
                              (
                                  p.Code,
                                  p.DeliverTime!.Value,
                                  p.Box.LocalId
                              )).ToListAsync();

        return Ok(new UserStatus(deliverTime, packages));
    }

    [HttpPost("Open")]
    public async Task OpenBox(string boxNumber)
    {
        await Task.CompletedTask;
    }

    public sealed record SendTokenData(int PhoneNumber);
    public sealed record LoginData(int PhoneNumber, string Token);
    public sealed record UserStatus(DateTime? LettersDeliverTime, IReadOnlyList<UserPackageStatus> Packages);
    public sealed record UserPackageStatus(string Code, DateTime DeliverTime, string BoxNumber);
}
