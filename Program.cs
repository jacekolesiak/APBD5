using APBD5;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IMockDb, MockDb>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("animals", (IMockDb mockDb) =>
{
    return Results.Ok(mockDb.GetAllAnimals());
});

app.MapGet("animals/{id}", (int id, IMockDb mockDb) =>
{
    var animal = mockDb.GetAllAnimals().FirstOrDefault(a => a.Id == id);
    if (animal == null)
        return Results.NotFound("Animal not found");
    return Results.Ok(animal);
});

app.MapPost("animals", (Animal animal, IMockDb mockDb) =>
{
    mockDb.AddAnimal(animal);
    return Results.Created($"/animals/{animal.Id}", animal);
});

app.MapPut("animals/{id}", (int id, Animal animal, IMockDb mockDb) =>
{
    var existingAnimal = mockDb.GetAllAnimals().FirstOrDefault(a => a.Id == id);
    if (existingAnimal == null)
        return Results.NotFound("Animal not found");
    
    existingAnimal.Name = animal.Name;
    existingAnimal.Category = animal.Category;
    existingAnimal.Mass = animal.Mass;
    existingAnimal.FurColor = animal.FurColor;

    return Results.Ok(existingAnimal);
});

app.MapDelete("animals/{id}", (int id, IMockDb mockDb) =>
{
    var animalToRemove = mockDb.GetAllAnimals().FirstOrDefault(a => a.Id == id);
    if (animalToRemove == null)
        return Results.NotFound("Animal not found");

    mockDb.RemoveAnimal(animalToRemove);
    return Results.NoContent();
});

app.MapGet("animals/{id}/visits", (int id, IMockDb mockDb) =>
{
    var animal = mockDb.GetAllAnimals().FirstOrDefault(a => a.Id == id);
    if (animal == null)
        return Results.NotFound("Animal not found");

    var visits = mockDb.GetAllVisits().Where(v => v.Animal == animal).ToList();
    return Results.Ok(visits);
});

app.MapPost("animals/{id}/visits", (int id, Visit visit, IMockDb mockDb) =>
{
    var animal = mockDb.GetAllAnimals().FirstOrDefault(a => a.Id == id);
    if (animal == null)
        return Results.NotFound("Animal not found");

    visit.Animal = animal;
    mockDb.AddVisit(visit);
    return Results.Created($"/animals/{id}/visits/{visit.Id}", visit);
});

app.Run();