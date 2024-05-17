using DynamicForm.DTOs.Common;
using DynamicForm.Enums;
using System.ComponentModel.DataAnnotations;

namespace DynamicForm.DTOs
{
    public class QuestionConfigurationDTO : BaseEntityDTO
    {
        [Required(ErrorMessage = "ProgramId is required")]
        public Guid ProgramId { get; set; }

        [Required(ErrorMessage = "ApplicationFormConfigurationId is required")]
        public Guid ApplicationFormConfigurationId { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public QuestionType Type { get; set; }
        public string TypeDescription { get; set; }

        [Required(ErrorMessage = "Question is required")]
        public string Question { get; set; }
        public ICollection<ChoiceDTO> Choices { get; set; }

        public bool EnableOtherOption { get; set; } = false;

        public int? ChoiceAllowed { get; set; }
    }

    public class CreateQuestionConfigurationDTO
    {
        [Required(ErrorMessage = "Type is required")]
        public QuestionType Type { get; set; }

        [Required(ErrorMessage = "Question is required")]
        public string Question { get; set; }
        public ICollection<ChoiceDTO>? Choices { get; set; }
        public bool EnableOtherOption { get; set; } = false;
        public int? ChoiceAllowed { get; set; }

    }

    public class ChoiceDTO
    {
        public string Value { get; set; }
    }
}
