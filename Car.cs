namespace CarInventory
{
	public class Car
	{
		//Auto-implemented properties
		//The Id Peroperty is the primary key
		public int Id { get; set; }
		public required string Make { get; set; }
		public required string Model { get; set; }
		public bool IsAvailable { get; set; } = true;
		public string? Secret { get; set; }
	}
}
