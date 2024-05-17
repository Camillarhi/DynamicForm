using DynamicForm.Models.Common;

namespace DynamicForm.Models
{
    public class ApplicationFormConfiguration : BaseEntity
    {
        public Guid ProgramId { get; set; }
        public FieldConfiguration FirstName { get; set; }
        public FieldConfiguration LastName { get; set; }
        public FieldConfiguration Email { get; set; }
        public FieldConfiguration Phone { get; set; }
        public FieldConfiguration Nationality { get; set; }
        public FieldConfiguration CurrentResidence { get; set; }
        public FieldConfiguration IdNumber { get; set; }
        public FieldConfiguration DateOfBirth { get; set; }
        public FieldConfiguration Gender { get; set; }
        public ICollection<QuestionConfiguration>? CustomQuestions { get; set; }
    }

    public class FieldConfiguration
    {
        public bool IsInternal { get; set; } = false;
        public bool IsVisible { get; set; } = false;
        public bool IsMandatory { get; set; } = false;
    }
}
