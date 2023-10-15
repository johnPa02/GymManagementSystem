using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Gym.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gym.Repositories.Implementation
{
	public class GenericRepository<T> : IDisposable, IGenericRepository<T> where T : class
	{
		private readonly ApplicationDbContext _context;
		private readonly DbSet<T> _dbSet;

		public GenericRepository(ApplicationDbContext context)
		{
			_context = context;
			_dbSet = _context.Set<T>();
		}

		public IEnumerable<T> GetAll(
			Expression<Func<T, bool>>? filter = null,
			Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
			string includeProperties = "")
		{
			IQueryable<T> query = _dbSet;

			if (filter != null)
			{
				query = query.Where(filter);
			}

			foreach (var includeProperty in includeProperties.Split
				(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProperty);
			}

			if (orderBy != null)
			{
				return orderBy(query).ToList();
			}
			else
			{
				return query.ToList();
			}
		}

		public T GetById(object id)
		{
			return _dbSet.Find(id);
		}

		public async Task<T> GetByIdAsync(object id)
		{
			return await _dbSet.FindAsync(id);
		}

		public void Add(T entity)
		{
			_dbSet.Add(entity);
			_context.SaveChanges();
		}

		public async Task<T> AddAsync(T entity)
		{
			_dbSet.Add(entity);
			await _context.SaveChangesAsync();
			return entity;
		}

		public void Update(T entity)
		{
			_dbSet.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
			_context.SaveChanges();
		}

		public async Task<T> UpdateAsync(T entity)
		{
			_dbSet.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return entity;
		}

		public void Delete(T entity)
		{
			if (_context.Entry(entity).State == EntityState.Detached)
			{
				_dbSet.Attach(entity);
			}
			_dbSet.Remove(entity);
			_context.SaveChanges();
		}

		public async Task<T> DeleteAsync(T entity)
		{
			if (_context.Entry(entity).State == EntityState.Detached)
			{
				_dbSet.Attach(entity);
			}
			_dbSet.Remove(entity);
			await _context.SaveChangesAsync();
			return entity;
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
