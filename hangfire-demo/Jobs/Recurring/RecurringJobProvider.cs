using Hangfire;
using hangfire_demo.Helper;
using hangfire_demo.Model;
using hangfire_demo.Services;
using Microsoft.EntityFrameworkCore;

namespace hangfire_demo.Jobs.Recurring
{
	public class RecurringJobProvider
	{
		IConfiguration? _config;
		IRepository<PriceTracker> _priceRepo;
		IRepository<Currency> _currencyRepo;
		public RecurringJobProvider(IConfiguration? config, IRepository<PriceTracker> price, IRepository<Currency> currency)
		{
			_config = config;
			_priceRepo = price;
			_currencyRepo = currency;
		}

		public async Task DailyCryptoPriceTrackerJob()
		{
			string rapidApiKey = _config.GetValue<string>("RapidApiKey");

			var cryptoToTracks = _currencyRepo.GetAll().ToList().Select(x=>x.Symbol).ToArray();
			List<PriceTracker> priceTrackerList = new List<PriceTracker>();

			foreach(var currency in cryptoToTracks) 
			{
				CyptoTrackerService tracker = new CyptoTrackerService(currency, rapidApiKey);
				var price = await tracker.GetDailyPriceFromExchange();
				price.CryptoCurrency = currency;
				priceTrackerList.Add(price);
			}

			_priceRepo.BulkInsert(priceTrackerList);

		}
	}
}
