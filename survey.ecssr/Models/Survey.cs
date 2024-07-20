namespace survey.ecssr.Models
{
    public class Survey
    {
        public Survey()
        {
            this.Questions = new HashSet<Question>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
