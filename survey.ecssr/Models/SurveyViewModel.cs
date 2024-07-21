
using System.ComponentModel.DataAnnotations;

public class SurveyViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool FormSubmitted { get; set; }
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

    public int? StepNumber { get; set; }

    public string? ExtraText { get; set; }


    public List<OptionsViewModel> OptionsViewModel { get; set; }
}

public class OptionsViewModel
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public string? Text { get; set; }
    public bool IsDeleted { get; set; }

    public bool IsSelected { get; set; }

    public string ExtraComments { get; set; }

    public string ExtraComments2 { get; set; }

    public bool HasExtraComment { get; set; }

    public bool HasExtraComment2 { get; set; }

}
public class AnswerViewModel
{
    public Guid Id { get; set; }
    public string AnswerText { get; set; }
}


