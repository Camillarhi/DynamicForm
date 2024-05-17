using System.ComponentModel.DataAnnotations;
using DynamicForm.DTOs.Common;

namespace DynamicForm.DTOs
{
    public class ProgramConfigurationDTO : BaseEntityDTO
    {
        [Required(ErrorMessage = "Program title is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Application Form is required")]
        public ApplicationFormConfigurationDTO ApplicationForm { get; set; }
    }

    public class CreateProgramConfigurationDTO
    {
        [Required(ErrorMessage = "Program title is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Application Form is required")]
        public CreateApplicationFormConfigurationDTO ApplicationForm { get; set; }
    }
}
