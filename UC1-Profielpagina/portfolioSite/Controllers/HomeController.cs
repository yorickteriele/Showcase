using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Showcase_Profielpagina.Models;

namespace Controllers;

public class HomeController : Controller {
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger) {
        _logger = logger;
    }

    public IActionResult Index() {
        var skills = new List<WorkExperience> {
            new("C#", "Expert", "Programming Language"),
            new("SQL", "Intermediate", "Database Management"),
            new("Unity", "Advanced", "Game Development"),
            new("Java", "Intermediate", "Programming Language")
        };
        var skillsTitle = "Vaardigheden";

        ViewData["SkillsInput"] = skills;
        ViewData["SkillsTitle"] = skillsTitle;

        var workExperiences = new List<WorkExperience> {
            new("Medewerker", "2023 - 2024", "Student aan huis, Zwolle"),
            new("Schaduwstage Operational Specialist Intelligence", "jan 2020 - feb 2021", "Politie Oost-Nederland"),
            new("Webdesigner", "2021 - Heden", "OmmerOase, Ommen"),
            new("Horecamedewerker/schoonmaker", "2021 - 2022", "Monkey Town, Zwolle"),
            new("Krantenbezorger", "2020 - 2021", "All-Inn, Zwolle")
        };
        var workExperiencesTitle = "Werkervaringen";

        ViewData["ExperiencesInput"] = workExperiences;
        ViewData["ExperiencesTitle"] = workExperiencesTitle;

        var aboutMeInput =
            "Ik ben een enthousiaste en leergierige student Software Engineering met een passie voor technologie en ontwikkeling. Nieuwe kennis opdoen en deze praktisch toepassen motiveert mij. Ik zoek actief naar manieren om mezelf te verbeteren en uitdagingen aan te gaan. Mijn doel is om met mijn vaardigheden en doorzettingsvermogen een waardevolle bijdrage te leveren aan uw organisatie.";
        var aboutMe = "Over Mij";

        ViewData["AboutMeInput"] = aboutMeInput;
        ViewData["AboutMeTitle"] = aboutMe;

        return View();
    }


    public IActionResult Privacy() {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}