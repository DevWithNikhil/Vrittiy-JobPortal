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
        var data = await _api.GetAsync("jobs");

        var jobs = JsonConvert.DeserializeObject<List<dynamic>>(data);

        return View(jobs);
    }


    public IActionResult Apply(int id)
    {
        ViewBag.JobId = id;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Apply(int jobId, IFormFile file)
    {
        var path = Path.Combine("wwwroot/resumes", file.FileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        await _api.PostAsync($"applications/apply?jobId={jobId}", new { });

        return RedirectToAction("Index");
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(string title, string description, string location)
    {
        await _api.PostAsync("jobs", new { title, description, location });

        return RedirectToAction("Index");
    }
}