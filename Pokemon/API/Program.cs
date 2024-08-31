using API;
using API.AutoGen;
using Microsoft.EntityFrameworkCore;
using API.Repositories;
using Microsoft.AspNetCore.Mvc.Formatters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PokemonContext>();
builder.Services.AddControllers();
builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();

    //AddDbContext<PokemonContext>
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
Console.WriteLine($"Using {ProjectConstants.DatabaseProvider} database provider");
QueryingPokemon();
app.Run();
//Console.WriteLine($"Using {ProjectConstants.DatabaseProvider} database provider");

static void QueryingPokemon()
{
    using (PokemonContext db = new())
    {
        Console.WriteLine("Pokemon and how many they have: ");
        IQueryable<Pokemon>? pokemons = db.Pokemons;
        if (pokemons is null)
        {
            Console.WriteLine("No categories found.");
            return;
        }
        foreach (Pokemon p in pokemons)
        {
            Console.WriteLine(p.Name);
        }

    }
}