using System;
using API.AutoGen;
using Microsoft.EntityFrameworkCore.ChangeTracking; // EntityEntry<T>
using System.Collections.Concurrent; // ConcurrentDictionary

namespace API.Repositories
{
	public class PokemonRepository : IPokemonRepository
	{
		private static ConcurrentDictionary<int, Pokemon>? PokemonsCache;

		private PokemonContext db;

		public PokemonRepository(PokemonContext injectedContext)
		{
			db = injectedContext;

			if(PokemonsCache is null)
			{
				PokemonsCache = new ConcurrentDictionary<int, Pokemon>(
					db.Pokemons.ToDictionary(p => p.Id));
			}

		}

		public async Task<Pokemon?> createAsync(Pokemon p)
        {
            //p.Id = p.Id.ToUpper();

            EntityEntry<Pokemon> added = await db.Pokemons.AddAsync(p);
            int affected = await db.SaveChangesAsync();

            if(affected == 1)
            {
                if (PokemonsCache is null) return p;

                return PokemonsCache.AddOrUpdate(p.Id, p, UpdateCache);
            }
            else
            {
                return null;
            }
        }

        private Pokemon UpdateCache(int id, Pokemon p)
        {
            Pokemon? old;
            if(PokemonsCache is not null)
            {
                if (PokemonsCache.TryGetValue(id, out old))
                {
                    if(PokemonsCache.TryUpdate(id, p, old))
                    {
                        return p;
                    }
                }
            }
            return null;
        }


        public Task<IEnumerable<Pokemon>> RetrieveAllAsync()
        {
            return Task.FromResult(PokemonsCache is null
                ? Enumerable.Empty<Pokemon>() : PokemonsCache.Values);
        }

        public Task<Pokemon?> RetrieveAsync(int id)
        {
            //id = id.ToUpper();

            if (PokemonsCache is null) return null;

            PokemonsCache.TryGetValue(id, out Pokemon? p);
            return Task.FromResult(p);
        }

        public async Task<Pokemon?> UpdateAsync(int id, Pokemon p)
        {
            //id = id.ToUpper();
            //p.Id = p.Id.ToUpper();

            db.Pokemons.Update(p);

            int affected = await db.SaveChangesAsync();

            if(affected == 1)
            {
                return UpdateCache(id, p);
            }
            return null;

        }

        public async Task<bool?> DeleteAsync(int id)
        {
            if (PokemonsCache is null) return null;

            //id = id.ToUpper();
            Pokemon? p = db.Pokemons.Find(id);
            if (p is null) return null;
            db.Pokemons.Remove(p);

            int affected = await db.SaveChangesAsync();

            if(affected == 1)
            {
                if (PokemonsCache is null) return null;
                return PokemonsCache.TryRemove(id, out p);
            }
            else
            {
                return null;
            }
        }
    }
}

