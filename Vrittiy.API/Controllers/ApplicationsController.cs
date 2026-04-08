using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vrittiy.Core.Entities;
using Vrittiy.Infrastructure.Data;

[ApiController]
[Route("api/[controller]")]
public class ApplicationsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ApplicationsController(AppDbContext context)
    {
        _context = context;
    }

    [Authorize(Roles = "User")]
    [HttpPost("apply")]
    public IActionResult Apply(int jobId)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        var application = new JobApplication
        {
            JobId = jobId,
            UserId = userId,
            ResumePath = "dummy.pdf"
        };

        _context.JobApplications.Add(application);
        _context.SaveChanges();

        return Ok("Applied Successfully");
    }

    [Authorize(Roles = "User")]
    [HttpGet("my")]
    public IActionResult MyApplications()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        var data = _context.JobApplications
            .Where(x => x.UserId == userId)
            .Select(x => new
            {
                x.Id,
                x.JobId,
                x.ResumePath
            })
            .ToList();

        return Ok(data);
    }
}