using AdsApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdsApi.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AdsController : ControllerBase
	{
		private readonly AdsApiContext _dbContext;

		public AdsController(AdsApiContext dbContext)
		{
			_dbContext = dbContext;
		}

		/// <summary>
		/// Retrieves a list of all adverts.
		/// </summary>
		/// <returns>List of adverts</returns>
		[HttpGet]
		[ProducesResponseType(typeof(List<Advert>), 200)]
		public async Task<IActionResult> GetAll()
		{
			var ads = await _dbContext.Ads.ToListAsync();
			return Ok(ads);
		}

		/// <summary>
		/// Retrieves a specific advert by ID.
		/// </summary>
		/// <param name="id">Advert ID</param>
		/// <returns>Advert</returns>
		[HttpGet("{id}")]
		[ProducesResponseType(typeof(Advert), 200)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> Get(int id)
		{
			var ad = await _dbContext.Ads.FindAsync(id);
			if (ad == null)
			{
				return NotFound();
			}
			return Ok(ad);
		}

		/// <summary>
		/// Creates a new advert.
		/// </summary>
		/// <param name="newAd">The advert to create</param>
		/// <returns>Newly created advert</returns>
		[HttpPost]
		[ProducesResponseType(typeof(Advert), 201)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> Post([FromBody] Advert newAd)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			newAd.TimePosted = DateTime.UtcNow; // Sets the TimePosted to the current time
			_dbContext.Ads.Add(newAd);
			await _dbContext.SaveChangesAsync();

			return CreatedAtAction(nameof(Get), new { id = newAd.Id }, newAd);
		}

		/// <summary>
		/// Deletes an advert by ID.
		/// </summary>
		/// <param name="id">Advert ID</param>
		/// <returns>HTTP Status indicating success or failure</returns>
		[HttpDelete("{id}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> Delete(int id)
		{
			var ad = await _dbContext.Ads.FindAsync(id);
			if (ad == null)
			{
				return NotFound();
			}

			_dbContext.Ads.Remove(ad);
			await _dbContext.SaveChangesAsync();

			return NoContent();
		}

		/// <summary>
		/// Updates an existing advert by ID.
		/// </summary>
		/// <param name="id">Advert ID</param>
		/// <param name="updatedAd">Updated advert details</param>
		/// <returns>Updated advert</returns>
		[HttpPut("{id}")]
		[ProducesResponseType(typeof(Advert), 200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> Update(int id, [FromBody] Advert updatedAd)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != updatedAd.Id)
			{
				return BadRequest("ID mismatch");
			}

			var ad = await _dbContext.Ads.FindAsync(id);
			if (ad == null)
			{
				return NotFound();
			}

			ad.Title = updatedAd.Title;
			ad.Description = updatedAd.Description;
			ad.Content = updatedAd.Content;

			_dbContext.Entry(ad).State = EntityState.Modified;
			await _dbContext.SaveChangesAsync();

			return Ok(ad);
		}
	}
}
