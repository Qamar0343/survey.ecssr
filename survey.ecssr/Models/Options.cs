namespace survey.ecssr.Models
{
    public class Options
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string? Text { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Question Question { get; set; }
    }
}
