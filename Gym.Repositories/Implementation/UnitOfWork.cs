using System;
using System.Collections.Generic;
using Gym.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gym.Repositories.Implementation
{
	public class UnitOfWork : IUnitOfWork, IDisposable
	{
		private readonly ApplicationDbContext _context;

		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;
		}

		public IGenericRepository<T> GenericRepository<T>() where T : class
		{
			IGenericRepository<T> repo = new GenericRepository<T>(_context);
			return repo;

		}

		public void Save()
		{
			_context.SaveChanges();
		}

		private bool _disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					_context.Dispose();
				}
			}
			_disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
