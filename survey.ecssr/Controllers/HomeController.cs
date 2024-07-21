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

            var query = _applicationDbContext
                .Question
                .Where(q => !q.IsDeleted)
                .ToList();

            var options = _applicationDbContext.Options.ToList();

            int id = survey.Id;
            var data = new SurveyViewModel
            {
                Id = id,
                Title = survey.Title,
                Description = survey.Description,
                FormSubmitted = false,
                QuestionViewModel = query
                .Select(q => new QuestionViewModel
                {
                    Text = q.Text,
                    Id = q.Id,
                    DisplayOrder = q.DisplayOrder,
                    ControlTypeId = q.ControlTypeId,
                    ExtraText = q.ExtraText,
                    StepNumber = q.StepNumber,
                    OptionsViewModel = options.Where(aa => aa.QuestionId == q.Id).Select(aa => new OptionsViewModel
                    {
                        Id = aa.Id,
                        Text = aa.Text,
                        QuestionId = aa.QuestionId,
                        HasExtraComment = aa.HasExtraComment,
                        HasExtraComment2 = aa.HasExtraComment2
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
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            var response = new Response
            {
                CreatedDate = DateTime.UtcNow,
                SurveyId = model.Id
            };

            _applicationDbContext.Response.Add(response);
            _applicationDbContext.SaveChanges();

            List<Answer> answers = new List<Answer>();

            foreach (var question in model.QuestionViewModel)
            {

                if (question.ControlTypeId == (int)ControlTypes.SingleLineTextBox)
                {
                    answers.Add(new Answer
                    {
                        OptionId = null,
                        AnswerValue = question.SelctedAnswer,
                        QuestionId = question.Id,
                        ResponseId = response.Id
                    });
                }
                else if (question.ControlTypeId == (int)ControlTypes.RadioButton)
                {
                    var optId = Convert.ToInt32(question.SelctedAnswer);
                    var opts = question.OptionsViewModel.FirstOrDefault(op => op.Id == optId);
                    if (opts != null)
                    {
                        answers.Add(new Answer
                        {
                            OptionId = opts.Id,
                            AnswerValue = opts.Text,
                            ExtraValue = opts.ExtraComments,
                            ExtraValue2 = opts.ExtraComments2,
                            QuestionId = question.Id,
                            ResponseId = response.Id
                        });
                    }

                }
                else if (question.ControlTypeId == (int)ControlTypes.CheckBoxList)
                {

                    var opts = question.OptionsViewModel.Where(opt => opt.IsSelected).ToList();
                    foreach (var opt in opts)
                    {
                        answers.Add(new Answer
                        {
                            OptionId = opt.Id,
                            AnswerValue = opt.Text,
                            ExtraValue = opt.ExtraComments,
                            ExtraValue2 = opt.ExtraComments2,
                            QuestionId = question.Id,
                            ResponseId = response.Id
                        });
                    }
                }

            }
            _applicationDbContext.Answer.AddRange(answers);
            _applicationDbContext.SaveChanges();

            model.FormSubmitted = true;
            return RedirectToAction("Success");
        }
        public IActionResult Success()
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
