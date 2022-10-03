﻿using MailBoxSystem.Models;
using MailBoxSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MailBoxSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class DeviceController : ControllerBase
{
    #region fields, ctor

    private readonly AppDbContext db;
    private readonly ISmsService smsService;

    public DeviceController(AppDbContext db, ISmsService smsService)
    {
        this.db = db ?? throw new ArgumentNullException(nameof(db));
        this.smsService = smsService ?? throw new ArgumentNullException(nameof(smsService));
    }

    #endregion


    [HttpGet("Boxes")]
    public async Task<ActionResult> Boxes()
    {
        var letterBoxes = await db.LetterBoxes
            .Select(b => new LetterBoxDto(b.LocalNumber, b.Line1, b.Line2))
            .ToListAsync();

        var packageBoxes = await db.PackageBoxes
            .Select(b => new PackageBoxDto(b.LocalNumber, b.Size))
            .ToListAsync();

        var data = new DeviceBoxes(letterBoxes, packageBoxes);
        return Ok(data);
    }

    [HttpPost("Authenticate")]
    public async Task<ActionResult> Authenticate(int boxNumber, string password)
    {
        var exists = await db.LetterBoxes.AnyAsync(b => b.LocalNumber == boxNumber && b.Owner.Password == password);
        return exists ? Ok() : NotFound();
    }

    [HttpPost("DeliverPack")]
    public async Task<ActionResult> DeliverPack(string code, int boxNumber)
    {
        var pack = await db.Packages.FirstOrDefaultAsync(b => b.Code == code);

        if (pack == null) return NotFound();

        var boxId = await (from b in db.PackageBoxes
                           where b.LocalNumber == boxNumber
                           select b.Id).FirstAsync();

        pack.BoxId = boxId;
        pack.DeliverTime = DateTime.Now;

        await db.SaveChangesAsync();
        return Ok();
    }

    public sealed record LetterBoxDto(int Number, string Line1, string Line2);
    public sealed record PackageBoxDto(int PhoneNumber, PackBoxSize Size);
    public sealed record DeviceBoxes(IReadOnlyList<LetterBoxDto> LetterBoxes, IReadOnlyList<PackageBoxDto> PackageBoxes);
}
