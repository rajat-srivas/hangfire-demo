using System.ComponentModel.DataAnnotations;

namespace hangfire_demo.Model
{
	public class EntityBase
	{
		[Key]
		public int Id { get; set; }
	}
}
