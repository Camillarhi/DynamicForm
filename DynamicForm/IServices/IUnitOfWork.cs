using Microsoft.EntityFrameworkCore;

namespace DynamicForm.IServices
{
    public interface IUnitOfWork
    {
        DbContext Context { get; }
        Task SaveChangesAsync();
    }
}
