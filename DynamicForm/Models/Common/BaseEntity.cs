using System.ComponentModel.DataAnnotations;

namespace DynamicForm.Models.Common
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
