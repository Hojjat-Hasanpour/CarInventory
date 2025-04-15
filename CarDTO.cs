using System.Diagnostics.CodeAnalysis;

namespace CarInventory
{
	public class CarDTO
	{
		public int Id { get; set; }
		public required string Make { get; set; }
		public required string Model { get; set; }
		public bool IsAvailable { get; set; } = true;

		public CarDTO()
		{
			
		}

		[SetsRequiredMembers]
		public CarDTO(Car car)
		{
			Id = car.Id;
			Make = car.Make;
			Model = car.Model;
			IsAvailable = car.IsAvailable;
		}
	}
}
