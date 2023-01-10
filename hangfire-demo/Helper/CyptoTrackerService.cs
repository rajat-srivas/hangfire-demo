using hangfire_demo.Model;
using Newtonsoft.Json;

namespace hangfire_demo.Helper
{
	public class CyptoTrackerService
	{
		private readonly string _currencyToFetch;
		private readonly string _key;

		public CyptoTrackerService(string currency, string key)
		{
			_currencyToFetch = currency;
			_key = key;
		}

		public async Task<List<Currency>> GetCurrencies()
		{
			var client = new HttpClient();
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri("https://coinranking1.p.rapidapi.com/coins?referenceCurrencyUuid=yhjMzLPhuIDl&timePeriod=24h&tiers%5B0%5D=1&orderBy=marketCap&orderDirection=desc&limit=50&offset=0"),
				Headers =
						{
							{ "X-RapidAPI-Key", _key },
							{ "X-RapidAPI-Host", "coinranking1.p.rapidapi.com" },
						},
			};
			using (var response = await client.SendAsync(request))
			{
				var currencies = new CurrencyData();
				response.EnsureSuccessStatusCode();
				var body = await response.Content.ReadAsStringAsync();
				if (!string.IsNullOrEmpty(body))
				{
					currencies = JsonConvert.DeserializeObject<CurrencyData>(body);
				}

				return currencies?.Data?.Coins;
			}
		}
		public async Task<PriceTracker> GetDailyPriceFromExchange()
		{
			var client = new HttpClient();
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri($"https://cryptopricetracker.p.rapidapi.com/{_currencyToFetch}"),
				Headers =
					{
						{ "X-RapidAPI-Key", _key },
						{ "X-RapidAPI-Host", "cryptopricetracker.p.rapidapi.com" },
					},
			};

			using (var response = await client.SendAsync(request))
			{
				response.EnsureSuccessStatusCode();
				var body = await response.Content.ReadAsStringAsync();

				PriceTracker price = new PriceTracker();

				if (!string.IsNullOrEmpty(body))
				{
					price = JsonConvert.DeserializeObject<PriceTracker>(body);
					price.PriceOn = DateTime.Now;
				}

				return price;
			}

		}
	}
}
