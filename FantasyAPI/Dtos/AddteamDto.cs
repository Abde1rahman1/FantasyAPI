using FantasyAPI.Models;

namespace FantasyAPI.Dtos
{
	public class AddteamDto
	{
		public string Name { get; set; }
		//public int TotalPoints { get; set; }

		public List<int>? Players { get; set; }
	}
}
