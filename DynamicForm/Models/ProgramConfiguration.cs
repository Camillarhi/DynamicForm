using DynamicForm.Models.Common;

namespace DynamicForm.Models
{
    public class ProgramConfiguration : BaseEntity
    {
        public string Description {  get; set; }
        public ApplicationFormConfiguration ApplicationForm { get; set; }
    }
}
