using System.ComponentModel.DataAnnotations;

namespace AdsApi.Data
{
	public class Advert
	{
		public int Id { get; set; }

		[Required]
		[StringLength(30, MinimumLength = 2)]
		public string Title { get; set; }

		[StringLength(30, MinimumLength = 2)]
		public string Description { get; set; }
		[Required]
		[StringLength(500, MinimumLength = 2)]
		public string Content { get; set; }

		[Required]
		public DateTime TimePosted { get; set; }
	}
}