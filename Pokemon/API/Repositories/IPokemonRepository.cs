using System;
using API.AutoGen;


namespace API.Repositories
{
	public interface IPokemonRepository
	{
		Task<Pokemon?> createAsync(Pokemon p);
		Task<IEnumerable<Pokemon>> RetrieveAllAsync();
		Task<Pokemon?> RetrieveAsync(int id);
		Task<Pokemon?> UpdateAsync(int id, Pokemon p);
		Task<bool?> DeleteAsync(int id);

	}
}

