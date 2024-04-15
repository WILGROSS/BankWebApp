using AdsApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace AdsApi.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AdsController(AdsApiContext dbContext) : ControllerBase
	{
		/// <summary>
		/// Bajskorv summary
		/// </summary>
		/// <param name="id">Advert ID</param>
		/// <returns>Advert</returns>
		[HttpGet("{id}")]
		[ProducesResponseType(typeof(Advert), 200)]
		[ProducesResponseType(400)]

		public async Task<IActionResult> Get(int id)
		{
			var ad = await dbContext.Ads.FindAsync(id);
			if (ad == null)
			{
				return BadRequest("Not found");
			}
			return Ok(ad);
		}
	}
}
