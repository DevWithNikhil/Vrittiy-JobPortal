using Microsoft.AspNetCore.Mvc;
using Vrittiy.API.DTOs;
using Vrittiy.Core.Entities;
using Vrittiy.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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


    [Authorize(Roles = "Recruiter")]
    [HttpPost]
    public IActionResult CreateJob(JobDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var job = new Job
        {
            Title = dto.Title,
            Description = dto.Description,
            Location = dto.Location,
            RecruiterId = userId
        };

        _context.Jobs.Add(job);
        _context.SaveChanges();

        return Ok("Job Created");
    }


    [HttpGet("{id}")]
    public IActionResult GetJobById(int id)
    {
        var job = _context.Jobs
            .Where(x => x.Id == id)
            .Select(x => new
            {
                x.Id,
                x.Title,
                x.Description,
                x.Location
            })
            .FirstOrDefault();

        if (job == null)
            return NotFound();

        return Ok(job);
    }
}
