namespace survey.ecssr.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Question Question { get; set; }
    }
}
