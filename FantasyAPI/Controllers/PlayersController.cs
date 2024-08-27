using FantasyAPI.Dtos;
using FantasyAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FantasyAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PlayersController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly List<string> AllPostitions = new List<string>
		{
			"GK", "CB", "RB", "LB", "CM", "RW", "LW", "CF"
		};

		public PlayersController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			var players = await _context.Players
				.OrderByDescending(p => p.Points)
				.Select(p => new PlayerDto
				{
					Id = p.Id,
					Name = p.Name,
					Points = p.Points,
					Position = p.Position
				})
				.ToListAsync();

			return Ok(players);
		}
		[HttpPost]
		public async Task<IActionResult> CreateAsync([FromBody] CreatePayer dto)
		{

			if (!AllPostitions.Contains(dto.Position))
			{
				return BadRequest("Invalid position.");
			}

			var player = new Player
			{
				
				Name = dto.Name,
				Points = dto.Points,
				Position = dto.Position
			};

			await _context.Players.AddAsync(player);
			await _context.SaveChangesAsync();

			return Ok(player);
		}
		
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdatePositionAsync(int id, [FromBody] CreatePayer dto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var player = await _context.Players.FindAsync(id);
			if (player == null)
			{
				return NotFound();
			}

			if (!AllPostitions.Contains(dto.Position))
			{
				return BadRequest("Invalid position.");
			}

			player.Position = dto.Position;
			await _context.SaveChangesAsync();

			return Ok(player);
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var player = await _context.Players.FindAsync(id);
			if (player == null)
			{
				return NotFound();
			}

			_context.Players.Remove(player);
			await _context.SaveChangesAsync();

			return NoContent();

		}
	}
}
