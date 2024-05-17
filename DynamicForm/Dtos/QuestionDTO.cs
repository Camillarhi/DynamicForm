using DynamicForm.Models;

namespace DynamicForm.Dtos
{
    public class QuestionDTO
    {
        public Guid CandidateApplicationId { get; set; }
        public Guid QuestionConfigurationId { get; set; }
        public string Answer { get; set; }
        public bool? isOther { get; set; } = false;
        public ICollection<Choice>? Choices { get; set; }
        public bool? YesNoAnswer { get; set; } = false;
        public DateTime? DateAnswer { get; set; }
        public double? NumberAnswer { get; set; }
    }
}
