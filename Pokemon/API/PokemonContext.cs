using static System.Console;
using API.AutoGen;
using Microsoft.EntityFrameworkCore;

namespace API
{
	public partial class PokemonContext : DbContext
	{
        public virtual DbSet<Pokemon> Pokemons { get; set; } = null!;

        protected override void OnConfiguring (DbContextOptionsBuilder
			optionBuilder)
		{
			if (ProjectConstants.DatabaseProvider == "SQLite")
			{
				string path = Path.Combine(Environment.CurrentDirectory,
					"Pokemon.db");

				optionBuilder.UseSqlite($"Filename={path}");

			}
		}
	}
}

