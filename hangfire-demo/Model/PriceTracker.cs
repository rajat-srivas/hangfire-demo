using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace hangfire_demo.Model
{
	public class PriceTracker : EntityBase
	{

		[JsonPropertyName("currency")]
		public string? CryptoCurrency { get; set; }
		public DateTime? PriceOn { get; set; }

		public string? Price { get; set; }

		public string? ChangeRate { get; set; }


	}
}
