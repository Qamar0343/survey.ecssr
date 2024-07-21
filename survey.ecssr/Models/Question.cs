using System.Reflection.Metadata.Ecma335;

namespace survey.ecssr.Models
{
    public class Question
    {
        public Question()
        {
            this.Answers = new HashSet<Options>();
        }
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public string Text { get; set; }
        public string? ExtraText { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsDeleted { get; set; }
        public int? ControlTypeId { get; set; }
        public int? StepNumber { get; set; }
        public virtual ICollection<Options> Answers { get; set; }
        public virtual Survey Survey { get; set; }
    }
}
