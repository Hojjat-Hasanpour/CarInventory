using Microsoft.EntityFrameworkCore;

namespace CarInventory
{
	public class CarDb: DbContext
	{
		public CarDb(DbContextOptions<CarDb> options) : base(options)
		{
		}

		//Creating car table in the DB
		public DbSet<Car> Cars { get; set; } = null!;
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Car>().HasKey(c => c.Id);
			//modelBuilder.Entity<Car>().Property(c => c.Make).IsRequired();
			//modelBuilder.Entity<Car>().Property(c => c.Model).IsRequired();
			//modelBuilder.Entity<Car>().Property(c => c.IsAvaiable).IsRequired().HasDefaultValue(true);
			//modelBuilder.Entity<Car>().Property(c => c.Secret).IsRequired(false);
		}
	}
	
}
