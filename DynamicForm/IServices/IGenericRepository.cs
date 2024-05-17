using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace DynamicForm.IServices
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<IList<T>> GetAll(
                    Expression<Func<T, bool>>? expression = null,
                    Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null
                    );
        public Task<T> GetOne(Expression<Func<T, bool>> expression);
        public Task<ActionResult<T>> Create(T entity);
        public Task<IActionResult> Update(Guid id, T entity);
        public Task<IActionResult> Delete(Guid id);
    }
}
