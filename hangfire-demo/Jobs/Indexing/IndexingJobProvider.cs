using hangfire_demo.Helper;
using hangfire_demo.Model;
using hangfire_demo.Services;

namespace hangfire_demo.Jobs.Indexing
{
	public class IndexingJobProvider
	{
		IConfiguration? _config;
		IRepository<Currency> _repository;
		public IndexingJobProvider(IConfiguration? config, IRepository<Currency> price)
		{
			_config = config;
			_repository = price;
		}

		public async Task IndexAllCurrencies()
		{
			string rapidApiKey = _config.GetValue<string>("RapidApiKey");
			CyptoTrackerService tracker = new CyptoTrackerService("", rapidApiKey);
			var currencies = await tracker.GetCurrencies();

			//this is for demo purpose, if run multiple times same record will be inserted again and again. Since it is indexing
			//maybe every time this is fired we can delete the records and insert a new one.
			//or make sure it is executed once
			_repository.BulkInsert(currencies);
		}
	}
}
