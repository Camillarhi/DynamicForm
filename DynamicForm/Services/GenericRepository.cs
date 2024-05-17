using DynamicForm.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DynamicForm.IServices;

namespace DynamicForm.Services
{
    public abstract class GenericRepository<T> : ControllerBase, IGenericRepository<T> where T : class
    {
        protected AppDbContext _context;
        protected DbSet<T> _db;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenericRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _db = _unitOfWork.Context.Set<T>();
            _mapper = mapper;
        }
        public async Task<ActionResult<T>> Create(T entity)
        {
            _db.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<IList<T>> GetAll(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            IQueryable<T> query = _db;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.AsNoTracking().ToListAsync();
        }


        public async Task<IActionResult> Delete(Guid id)
        {
            var entity = await _db.FindAsync(id);
            if (entity != null)
            {
                _db.Remove(entity);
                await _unitOfWork.SaveChangesAsync();
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        public virtual async Task<T> GetOne(Expression<Func<T, bool>> expression)
        {
            IQueryable<T> query = _db;
            return await query.AsNoTracking()
                .FirstOrDefaultAsync(expression);
        }

        public async Task<IActionResult> Update(Guid id, T entity)
        {
            var existingOrder = await _db.FindAsync(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            _unitOfWork.Context.Entry(existingOrder).CurrentValues.SetValues(entity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }
    }
}
