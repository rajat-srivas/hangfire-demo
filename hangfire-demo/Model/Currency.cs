using System.Text.Json.Serialization;

namespace hangfire_demo.Model
{
	public class Currency : EntityBase
	{
		public string? Uuid { get; set; }
		public string? Symbol { get; set; }
		public string? Name { get; set; }
		public string? Color { get; set; }
		public string? IconUrl { get; set; }
		public string? MarketCap { get; set; }
		public string? Price { get; set; }
		public string? BtcPrice { get; set; }
		public int ListedAt { get; set; }
		public string? Change { get; set; }
		public int Rank { get; set; }
		public string? CoinRankingUrl { get; set; }

	}

	public class Data
	{
		[JsonPropertyName("coins")]
		public List<Currency>? Coins { get; set; }
	}

	public class CurrencyData
	{
		[JsonPropertyName("data")]
		public Data? Data { get; set; }
	}
}
