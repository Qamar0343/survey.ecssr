using System.Security.Principal;

namespace survey.ecssr.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int? OptionId { get; set; }
        public string? AnswerValue { get; set; }
        public string? ExtraValue { get; set; } 
        public string? ExtraValue2 { get; set; }
        public int QuestionId { get; set; }
        public int ResponseId { get; set; }
    }
}
