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
            //var query = from s in _applicationDbContext.Survey
            //            join q in _applicationDbContext.Question
            //            on s.Id equals q.SurveyId
            //            join a in _applicationDbContext.Answer
            //            on q.Id equals a.QuestionId
            //            where !s.IsDeleted && !q.IsDeleted
            //            select new SurveyViewModel
            //            {
            //                Title = s.Title,
            //                Description = s.Description,
            //            };
            //var data = query.ToList();
            var survey = _applicationDbContext.Survey.FirstOrDefault(s => s.Id == 1);
            var query = _applicationDbContext.Question.Where(q => !q.IsDeleted)
                .ToList();

            //var query = from q in _applicationDbContext.Question
            //         join a in _applicationDbContext.Answer
            //         on q.Id equals a.QuestionId
            //         where !q.IsDeleted && q.SurveyId == 1
            //         select q;

            var data = new SurveyViewModel
            {
                Id = survey.Id,
                Title = survey.Title,
                Description = survey.Description,
                QuestionViewModel = query
                .Select(q => new QuestionViewModel
                {
                    Text = q.Text,
                    DisplayOrder = q.DisplayOrder,
                    //AnswerViewModel = _applicationDbContext.Answer
                    //.Where(bb => bb.Id == q.Id && !bb.IsDeleted)
                    //.Select(dd => new AnswerViewModel
                    //{
                    //    Text = dd.Text
                    //}).ToList()
                })
                .OrderBy(o => o.DisplayOrder)
                .ToList()
            };
            return View(data);
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
