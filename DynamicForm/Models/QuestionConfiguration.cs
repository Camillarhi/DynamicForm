using DynamicForm.Enums;
using DynamicForm.Models.Common;

namespace DynamicForm.Models
{
    public class QuestionConfiguration : BaseEntity
    {
        public Guid ProgramId { get; set; }
        public Guid ApplicationFormConfigurationId { get; set; }
        public QuestionType Type { get; set; }
        public string TypeDescription { get; set; }
        public string Question { get; set; }
        public ICollection<Choice>? Choices { get; set; }
        public bool EnableOtherOption { get; set; } = false;
        public int? ChoiceAllowed { get; set; }
    }

    public class Choice
    {
        public string Value { get; set; }
    }
}
