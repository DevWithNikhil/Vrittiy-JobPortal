using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Vrittiy.Web.Services;

public class AccountController : Controller
{
    private readonly ApiService _api;

    public AccountController(ApiService api)
    {
        _api = api;
    }

    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var response = await _api.PostAsync("auth/login", new { email, password });

        dynamic data = JsonConvert.DeserializeObject(response);

        HttpContext.Session.SetString("JWToken", (string)data.token);
        HttpContext.Session.SetString("UserRole", (string)data.role);
        HttpContext.Session.SetString("UserName", (string)data.name);

        if ((string)data.role == "Recruiter")
            return RedirectToAction("Create", "Jobs");

        else
            return RedirectToAction("Index", "Jobs");
    }

    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(string name, string email, string password, string role)
    {
        await _api.PostAsync("auth/register", new { name, email, password, role });

        return RedirectToAction("Login");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}