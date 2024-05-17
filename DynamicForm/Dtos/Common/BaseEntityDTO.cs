using System.ComponentModel.DataAnnotations;

namespace DynamicForm.DTOs.Common
{
    public abstract class BaseEntityDTO
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "CreatedDate is required")]
        public DateTime CreatedDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
