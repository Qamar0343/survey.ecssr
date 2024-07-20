
public class SurveyViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public List<QuestionViewModel> QuestionViewModel { get; set; }
}
public class QuestionViewModel
{
    public int Id { get; set; }
    public int SurveyId { get; set; }
    public string Text { get; set; }
    public int DisplayOrder { get; set; }
    public string SelctedAnswer { get; set; }
    public bool IsDeleted { get; set; }
    public int? ControlTypeId { get; set; }

    public List<OptionsViewModel> OptionsViewModel { get; set; }
}

public class OptionsViewModel
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public string? Text { get; set; }
    public bool IsDeleted { get; set; }
}

