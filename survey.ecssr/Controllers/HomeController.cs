using Microsoft.AspNetCore.Mvc;
using survey.ecssr.Models;
using System.Diagnostics;

namespace survey.ecssr.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _applicationDbContext;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
        }

        public IActionResult Index()
        {
            
            var survey = _applicationDbContext.Survey.FirstOrDefault(s => s.Id == 1);

            var query = _applicationDbContext.Question.Where(q => !q.IsDeleted)
                .ToList();

            var options = _applicationDbContext.Options.ToList();

            int id = survey.Id;
            var data = new SurveyViewModel
            {
                Id = id,
                Title = survey.Title,
                Description = survey.Description,
                QuestionViewModel = query
                .Select(q => new QuestionViewModel
                {
                    Text = q.Text,
                    Id = q.Id,
                    DisplayOrder = q.DisplayOrder,
                    ControlTypeId = q.ControlTypeId,
                    OptionsViewModel = options.Where(aa => aa.QuestionId == q.Id).Select(aa => new OptionsViewModel
                    {
                        Id = aa.Id,
                        Text = aa.Text,
                        QuestionId = aa.QuestionId,
                    }).ToList()
                })
                .OrderBy(o => o.DisplayOrder)
                .ToList()
            };
            return View(data);
        }
        [HttpPost]
        public IActionResult Index(SurveyViewModel model)
        {
           return View(model);
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
