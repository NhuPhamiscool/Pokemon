using System;
using Microsoft.AspNetCore.Mvc;
using API.AutoGen;
using API.Repositories;

namespace API.Controllers
{
	[ApiController]
	[Route("[controller]")]

	public class PokemonController : ControllerBase
	{
		private readonly IPokemonRepository repo;

		public PokemonController(IPokemonRepository repo) 
		{
			this.repo = repo;
		}

		[HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        //GET /Pokemon
        public async Task<IEnumerable<Pokemon>> Get(string? species)
		{
			if(string.IsNullOrWhiteSpace(species))
			{
				return await repo.RetrieveAllAsync();
			}
			else
			{
				return (await repo.RetrieveAllAsync())
					.Where(p => p.Species == species);
			}
		}

		//[HttpGet("{species}")]
		//public IEnumerable<Pokemon> Get(string species, int range)
		//{
		//	return Enumerable.Range(1, range).Select(id =>
		//	new Pokemon
		//	{
		//		Id = id,
		//		Name = "koko",
		//		Species = species

		//	})
		//		.ToArray();
		//}
	}


}

