namespace FantasyAPI.Models
{
	public class Team
	{


    public int Id { get; set; }
    public string Name { get; set; }

    // Navigation property
    public List<Player> Players { get; set; } = new List<Player>();

    public int TotalPoints { get; set; }




	}
}
