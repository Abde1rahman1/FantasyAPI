using FantasyAPI.Models;

namespace FantasyAPI.Dtos
{
	public class TeamDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int TotalPoints { get; set; }
		public List<PlayerDto> Players { get; set; }
	}

}
