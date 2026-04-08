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

        return RedirectToAction("Index", "Jobs");
    }

    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(string name, string email, string password, string role)
    {
        await _api.PostAsync("auth/register", new { name, email, password, role });

        return RedirectToAction("Login");
    }
}