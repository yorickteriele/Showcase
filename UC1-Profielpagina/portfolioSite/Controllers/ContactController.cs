using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Showcase_Contactpagina.Models;

namespace Controllers;

public class ContactController : Controller
{
    private readonly string _recaptchaSecretKey;
    private readonly HttpClient _httpClient;

    public ContactController(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://localhost:5161");

        _recaptchaSecretKey = configuration["Recaptcha:SecretKey"] 
                              ?? throw new ArgumentNullException("Recaptcha secret key is missing!");
    }

    public ActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Index(Contactform form, string gRecaptchaResponse)
    {
        if (!ModelState.IsValid)
        {
            TempData["Message"] = "De ingevulde velden voldoen niet aan de gestelde voorwaarden.";
            return View(form);
        }

        var recaptchaValid = await VerifyRecaptcha(gRecaptchaResponse);
        if (!recaptchaValid)
        {
            TempData["Message"] = "reCAPTCHA validatie mislukt. Probeer opnieuw.";
            return View(form); 
        }

        var settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        var json = JsonConvert.SerializeObject(form, settings);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/api/mail", content);

        TempData["Message"] = response.IsSuccessStatusCode 
            ? "Het contactformulier is verstuurd." 
            : "Er is iets misgegaan bij het versturen.";

        return RedirectToAction("Index"); 
    }

    private async Task<bool> VerifyRecaptcha(string token)
    {
        var url = $"https://www.google.com/recaptcha/api/siteverify?secret={_recaptchaSecretKey}&response={token}";
        var response = await _httpClient.PostAsync(url, null);
        var json = await response.Content.ReadAsStringAsync();
        var recaptchaResponse = JsonConvert.DeserializeObject<RecaptchaResponse>(json);
        return recaptchaResponse?.Success ?? false;
    }

    private class RecaptchaResponse
    {
        [JsonProperty("success")] public bool Success { get; set; }

        [JsonProperty("challenge_ts")] public string ChallengeTs { get; set; }

        [JsonProperty("hostname")] public string Hostname { get; set; }
    }
}