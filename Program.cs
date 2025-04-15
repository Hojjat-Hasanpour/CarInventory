using CarInventory;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Dependency Injection - Will construct a CarDb object when it needed
// Allow the CarDb to be injected into the methods that need it
builder.Services.AddDbContext<CarDb>(options => options.UseInMemoryDatabase("CarList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

RouteGroupBuilder cars = app.MapGroup("/cars");

cars.MapGet("/", GetAllCars);
cars.MapGet("/{id}", GetCar);
cars.MapPost("/", CreateCar);
cars.MapDelete("/{id}", DeleteCar);
cars.MapPut("/{id}", UpdateCar);
cars.MapGet("/available",GetAvailableCars);

app.MapGet("/", () => "Car Inventory System!");
app.Run();

static async Task<IResult> GetAllCars(CarDb db)
{
	return TypedResults.Ok(await db.Cars.Select(c => new CarDTO(c)).ToArrayAsync());
}

static async Task<IResult> CreateCar(Car car, CarDb db)
{
	db.Cars.Add(car);
	await db.SaveChangesAsync();
	var dto = new CarDTO(car);
	return TypedResults.Created($"/cars/{car.Id}", dto);
}

static async Task<IResult> GetCar(CarDb db, int id)
{
	return await db.Cars.FindAsync(id)
		is Car car
		? TypedResults.Ok(new CarDTO(car))
		: TypedResults.NotFound();
}

static async Task<IResult> DeleteCar(CarDb db, int id)
{
	if (await db.Cars.FindAsync(id) is Car car)
	{
		db.Cars.Remove(car);
		await db.SaveChangesAsync();
		return TypedResults.NoContent();
	}

	return TypedResults.NotFound();
}

static async Task<IResult> UpdateCar(int id, CarDTO carDTO, CarDb db)
{
	var car = await db.Cars.FindAsync(id);
	if (car is null) return TypedResults.NotFound();

	car.Make = carDTO.Make;
	car.Model = carDTO.Model;
	car.IsAvailable = carDTO.IsAvailable;
	await db.SaveChangesAsync();
	return TypedResults.Ok(new CarDTO(car));
}

static async Task<IResult> GetAvailableCars(CarDb db)
{
	return TypedResults.Ok(await db.Cars.Where(car=> car.IsAvailable).Select(c=>new CarDTO(c)).ToArrayAsync());
}