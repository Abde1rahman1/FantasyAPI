using FantasyAPI.Dtos;
using FantasyAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FantasyAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TeamsController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		public TeamsController(ApplicationDbContext context)
		{
			_context = context;
		}


		[HttpGet]
		public async Task<IActionResult> GetAllTeams()
		{
			var teams = await _context.Teams
				.Select(t => new TeamDto
				{
					Id = t.Id,
					Name = t.Name,
					TotalPoints = t.Players.Sum(p => p.Points), 
					Players = t.Players.Select(p => new PlayerDto
					{
						Id = p.Id,
						Name = p.Name,
						Position = p.Position,
						Points = p.Points
					}).ToList()
				})
				.OrderByDescending(t => t.TotalPoints) 
				.ToListAsync();

			return Ok(teams);
		}



		[HttpGet("{id}")]
		public async Task<IActionResult> GetTeamById(int id)
		{
			var team = await _context.Teams
				.Where(t => t.Id == id)
				.Select(t => new TeamDto
				{
					Id = t.Id,
					Name = t.Name,
					TotalPoints = t.Players.Sum(p => p.Points), 
					Players = t.Players.Select(p => new PlayerDto
					{
						Id = p.Id,
						Name = p.Name,
						Position = p.Position,
						Points = p.Points
					}).ToList()
				})
				.FirstOrDefaultAsync();

			if (team == null)
			{
				return NotFound();
			}

			return Ok(team);
		}

		[HttpPost("create")]
		public async Task<IActionResult> CreateTeam([FromBody] AddteamDto dto)
		{
			// Validate the input
			if (dto == null || string.IsNullOrWhiteSpace(dto.Name) || !dto.Players.Any())
			{
				return BadRequest("Invalid team data.");
			}

			var team = new Team
			{
				Name = dto.Name,
				Players = new List<Player>(),
				TotalPoints = 0
			};

			var players = await _context.Players
				.Where(p => dto.Players.Contains(p.Id))
				.ToListAsync();

			team.Players.AddRange(players);
			team.TotalPoints = players.Sum(p => p.Points);

			_context.Teams.Add(team);
			await _context.SaveChangesAsync();

			return Ok(team);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateTeam(int id, [FromBody] AddteamDto dto)
		{
		
			var team = await _context.Teams.Include(t => t.Players).FirstOrDefaultAsync(t => t.Id == id);

			if (team == null)
			{
				return NotFound();
			}

			team.Name = dto.Name;

			team.Players.Clear();

			var newPlayers = await _context.Players
				.Where(p => dto.Players.Contains(p.Id))
				.ToListAsync();

			team.Players.AddRange(newPlayers);

			team.TotalPoints = newPlayers.Sum(p => p.Points);

			_context.Teams.Update(team);
			await _context.SaveChangesAsync();

			return Ok(team);
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTeam(int id)
		{
			var team = await _context.Teams.FindAsync(id);
			if (team == null)
			{
				return NotFound();
			}

			_context.Teams.Remove(team);
			await _context.SaveChangesAsync();

			return Ok(team); 
		}

		[HttpGet("hightestTeam/")]
		public async Task<IActionResult> GetTheHightestPoints()
		{
			var teams = await _context.Teams
				.Select(t => new TeamDto
				{
					Id = t.Id,

					TotalPoints = t.Players.Sum(p => p.Points)
				}).ToListAsync();

			var TheHightestTeam = teams[0].TotalPoints;
			int id = teams[0].Id;
			foreach (var team in teams)
			{
				if (team.TotalPoints > TheHightestTeam)
				{
					TheHightestTeam = team.TotalPoints;
					id = team.Id;
					
				}
			}
			var HightestTeam = await _context.Teams
					.Where(t => t.Id == id)
					.Select(t => new TeamDto
					{
						Id = t.Id,
						Name = t.Name,
						TotalPoints = t.Players.Sum(p => p.Points),
						Players = t.Players.Select(p => new PlayerDto
						{
							Id = p.Id,
							Name = p.Name,
							Position = p.Position,
							Points = p.Points
						}).ToList()
					})
					.FirstOrDefaultAsync();
			return Ok(HightestTeam);
		}

		
	}
}
