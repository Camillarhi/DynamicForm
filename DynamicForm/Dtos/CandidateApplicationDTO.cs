using DynamicForm.DTOs.Common;

namespace DynamicForm.Dtos
{
    public class CandidateApplicationDTO : BaseEntityDTO
    {
        public Guid ProgramId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Nationality { get; set; }
        public string CurrentResidence { get; set; }
        public string IdNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public ICollection<QuestionDTO>? AdditionalQuestions { get; set; }
    }

    public class CreateCandidateApplicationDTO
    {
        public Guid ProgramId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Nationality { get; set; }
        public string CurrentResidence { get; set; }
        public string IdNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public ICollection<QuestionDTO>? AdditionalQuestions { get; set; }
    }
}
