using DynamicForm.Models;
using Microsoft.EntityFrameworkCore;

namespace DynamicForm.IServices
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }
        Task SaveChangesAsync();
    }
}
