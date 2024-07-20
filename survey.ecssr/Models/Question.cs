namespace survey.ecssr.Models
{
    public class Question
    {
        public Question()
        {
            this.Answers = new HashSet<Answer>();
        }
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public string Text { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
        public virtual Survey Survey { get; set; }
    }
}
