using hangfire_demo.Model;

namespace hangfire_demo.Services
{
	public interface IRepository<T> where T : EntityBase
	{
		IEnumerable<T> GetAll();
		void Insert(T entity);
		void BulkInsert(List<T> entities);
	}
}
