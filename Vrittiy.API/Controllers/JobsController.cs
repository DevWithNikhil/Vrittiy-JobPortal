using Microsoft.AspNetCore.Mvc;
using Vrittiy.API.DTOs;
using Vrittiy.Core.Entities;
using Vrittiy.Infrastructure.Data;

namespace Vrittiy.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobsController : ControllerBase
{
    private readonly AppDbContext _context;

    public JobsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetJobs()
    {
        var jobs = _context.Jobs.ToList();

        return Ok(jobs);
    }

    [HttpPost]
    public IActionResult CreateJob(JobDto dto)
    {
        var job = new Job
        {
            Title = dto.Title,
            Description = dto.Description,
            Location = dto.Location,
            RecruiterId = 1 // temporary hardcoded
        };

        _context.Jobs.Add(job);
        _context.SaveChanges();

        return Ok(new
        {
            message = "Job Created Successfully"
        });
    }
}
