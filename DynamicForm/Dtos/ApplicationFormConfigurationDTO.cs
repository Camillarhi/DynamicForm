using System.ComponentModel.DataAnnotations;
using DynamicForm.DTOs.Common;

namespace DynamicForm.DTOs
{
    public class ApplicationFormConfigurationDTO : BaseEntityDTO
    {
        [Required(ErrorMessage = "ProgramId is required")]
        public Guid ProgramId { get; set; }

        [Required(ErrorMessage = "FirstName is required")]
        public FieldConfigurationDTO FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public FieldConfigurationDTO LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public FieldConfigurationDTO Email { get; set; }

        public FieldConfigurationDTO Phone { get; set; }

        public FieldConfigurationDTO Nationality { get; set; }

        public FieldConfigurationDTO CurrentResidence { get; set; }

        public FieldConfigurationDTO IdNumber { get; set; }

        public FieldConfigurationDTO DateOfBirth { get; set; }

        public FieldConfigurationDTO Gender { get; set; }

        public ICollection<QuestionConfigurationDTO>? CustomQuestions { get; set; }
    }

    public class CreateApplicationFormConfigurationDTO
    {
        [Required(ErrorMessage = "FirstName is required")]
        public FieldConfigurationDTO FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public FieldConfigurationDTO LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public FieldConfigurationDTO Email { get; set; }

        public FieldConfigurationDTO Phone { get; set; }

        public FieldConfigurationDTO Nationality { get; set; }

        public FieldConfigurationDTO CurrentResidence { get; set; }

        public FieldConfigurationDTO IdNumber { get; set; }

        public FieldConfigurationDTO DateOfBirth { get; set; }

        public FieldConfigurationDTO Gender { get; set; }

        public ICollection<CreateQuestionConfigurationDTO>? CustomQuestions { get; set; }
    }

    public class FieldConfigurationDTO
    {
        public bool IsInternal { get; set; } = false;
        public bool IsVisible { get; set; } = false;
        public bool IsMandatory { get; set; } = false;
    }
}
