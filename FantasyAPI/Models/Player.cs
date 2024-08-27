using System.Text.Json.Serialization;

namespace FantasyAPI.Models
{
	public class Player
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Position { get; set; }
		public int Points { get; set; }

		[JsonIgnore]
		public Team? Team { get; set; }
		public int? TeamId { get; set; }

	}
}
