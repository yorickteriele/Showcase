using Microsoft.AspNetCore.Mvc;
using Showcase_Profielpagina.Models;
using System.Diagnostics;

namespace Showcase_Profielpagina.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index() {
            var workExperiences = new List<WorkExperience>{
                new WorkExperience("Medewerker", "2023 - 2024", "Student aan huis, Zwolle"),
                new WorkExperience("Schaduwstage Operational Specialist Intelligence", "jan 2020 - feb 2021", "Politie Oost-Nederland"),
                new WorkExperience("Webdesigner", "2021 - Heden", "OmmerOase, Ommen"),
                new WorkExperience("Horecamedewerker/schoonmaker", "2021 - 2022", "Monkey Town, Zwolle"),
                new WorkExperience("Krantenbezorger", "2020 - 2021", "All-Inn, Zwolle")
            };

            ViewData["Experiences"] = workExperiences;

            string aboutMe = "Ik ben een enthousiaste en leergierige student Software Engineering met een passie voor technologie en ontwikkeling. Nieuwe kennis opdoen en deze praktisch toepassen motiveert mij. Ik zoek actief naar manieren om mezelf te verbeteren en uitdagingen aan te gaan. Mijn doel is om met mijn vaardigheden en doorzettingsvermogen een waardevolle bijdrage te leveren aan uw organisatie.";

            ViewData["AboutMe"] = aboutMe;

            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

