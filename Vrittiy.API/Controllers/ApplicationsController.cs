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


    [HttpPost("apply")]
    [Authorize(Roles = "User")]
    public IActionResult Apply(int jobId, string resumePath)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        var application = new JobApplication
        {
            JobId = jobId,
            UserId = userId,
            ResumePath = resumePath
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
                JobTitle = x.Job.Title,
                JobLocation = x.Job.Location,
                x.ResumePath
            })
            .ToList();

        return Ok(data);
    }


    [Authorize(Roles = "Recruiter")]
    [HttpGet("job/{jobId}")]
    public IActionResult GetApplicationsByJob(int jobId)
    {
        var data = _context.JobApplications
            .Where(x => x.JobId == jobId)
            .Select(x => new
            {
                x.Id,
                UserName = x.User.Name,
                UserEmail = x.User.Email,
                Resume = x.ResumePath
            })
            .ToList();

        return Ok(data);
    }


    [Authorize(Roles = "Recruiter")]
    [HttpGet("my-jobs")]
    public IActionResult MyJobApplications()
    {
        var recruiterId = int.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier).Value
        );

        var data = _context.Jobs
            .Where(j => j.RecruiterId == recruiterId)
            .Select(j => new
            {
                JobId = j.Id,
                Title = j.Title,
                Applications = j.Applications.Select(a => new
                {
                    a.User.Name,
                    a.User.Email,
                    a.ResumePath
                })
            })
            .ToList();

        return Ok(data);
    }
}