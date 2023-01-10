using hangfire_demo.Context;
using hangfire_demo.Model;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Security.Principal;

namespace hangfire_demo.Services
{
	public class Repository<T> : IRepository<T> where T : EntityBase
	{
		public CoreDbContext _context;

		public Repository(CoreDbContext context) 
		{ 
			_context= context;
		}

		public virtual IEnumerable<T> GetAll()
		{
			return _context.Set<T>().ToList();
		}

		public virtual void Insert(T entity)
		{
			_context.Set<T>().Add(entity);
			_context.SaveChanges();
		}

		public virtual void BulkInsert(List<T> entities)
		{
			_context.Set<T>().AddRange(entities);
			_context.SaveChanges();
		}
	}
}
