using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Vrittiy.Web.Services;

public class JobsController : Controller
{
    private readonly ApiService _api;

    public JobsController(ApiService api)
    {
        _api = api;
    }

    public async Task<IActionResult> Index()
    {
        var role = HttpContext.Session.GetString("UserRole");

        if (role == "Recruiter")
        {
            return RedirectToAction("Index", "Home");
        }

        var data = await _api.GetAsync("jobs");
        var jobs = JsonConvert.DeserializeObject<List<dynamic>>(data);

        return View(jobs);
    }

    public async Task<IActionResult> Apply(int id)
    {
        var role = HttpContext.Session.GetString("UserRole");

        if (role != "User")
        {
            return RedirectToAction("Index", "Home");
        }

        var data = await _api.GetAsync($"jobs/{id}");

        var job = JsonConvert.DeserializeObject<dynamic>(data);

        return View(job);
    }

    [HttpPost]
    public async Task<IActionResult> Apply(int jobId, IFormFile file)
    {
        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

        
        var filePath = Path.Combine(Directory.GetCurrentDirectory(),
            "wwwroot/resumes", fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        await _api.PostAsync(
            $"applications/apply?jobId={jobId}&resumePath={fileName}",
            new { }
        );

        return RedirectToAction("Index");
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(string title, string description, string location)
    {
        await _api.PostAsync("jobs", new { title, description, location });

        return RedirectToAction("Index");
    }


    public async Task<IActionResult> MyApplications()
    {
        var data = await _api.GetAsync("applications/my");

        var apps = JsonConvert.DeserializeObject<List<dynamic>>(data);

        return View(apps);
    }

    public async Task<IActionResult> MyPostedJobs()
    {
        var data = await _api.GetAsync("applications/my-jobs");

        var jobs = JsonConvert.DeserializeObject<List<dynamic>>(data);

        return View(jobs);
    }
}